using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using DeepCodeAnalytics.Application.DTOs;
using DeepCodeAnalytics.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Extensions.Http;

namespace DeepCodeAnalytics.Infrastructure.Services;

/// <summary>
/// Groq API ile HTTP üzerinden iletişim kuran Infrastructure servis sınıfıdır.
/// IGeminiService arayüzünü uygular; sınıf adı UI uyumluluğu için korunmuştur.
/// Tüm HTTP hataları ve JSON parse hataları bu sınıf içinde yakalanır,
/// uygulama asla crash olmaz; hata bilgisi GeminiAnalysisResult.Failure() ile döner.
/// </summary>
public class GeminiService : IGeminiService
{
    // HTTP isteklerini yapmak için kullanılan client (DI Container tarafından yönetilir)
    private readonly HttpClient _httpClient;

    // appsettings.json dosyasındaki "Gemini:ApiKey" alanından okunan API anahtarı
    private readonly string _apiKey;

    // (Deniz) Polly retry policy'si: 429 ve 5xx hatalarında devreye girer.
    // Bu policy static tanımlandığından her istek için yeniden oluşturulmaz (performans).
    private static readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy =
        HttpPolicyExtensions
            // (Deniz) Hangi durumlarda yeniden deneneceğini tanımla:
            // - HttpRequestException (ağ kopması, DNS hatası)
            // - 5xx sunucu hataları
            // - 429 Too Many Requests (Rate Limit)
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
            // (Deniz) WaitAndRetryAsync: Exponential Backoff ile 3 kez dener.
            // 1. deneme → 2 saniye bekle
            // 2. deneme → 4 saniye bekle
            // 3. deneme → 8 saniye bekle
            // Jitter (rastgele milisaniye) eklenerek "thundering herd" problemi önlenir.
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))   // 2^1, 2^2, 2^3
                    + TimeSpan.FromMilliseconds(new Random().Next(0, 500)), // Jitter
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    // (Deniz) Her retry denemesinde konsola bilgi yaz; production'da ILogger kullanılabilir.
                    Console.WriteLine(
                        $"[Retry #{retryAttempt}] Groq API isteği başarısız " +
                        $"(HTTP {(int?)outcome.Result?.StatusCode} / {outcome.Exception?.Message}). " +
                        $"{timespan.TotalSeconds:F1} saniye sonra tekrar denenecek...");
                });

    // Constructor: HttpClient ve IConfiguration DI Container tarafından dışarıdan enjekte edilir
    public GeminiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        // API Key appsettings.json'da tanımlı değilse uygulama başlarken hata fırlatır (Fail-Fast prensibi)
        // Bu sayede çalışma zamanında sessiz hata yerine anında fark edilir
        _apiKey = configuration["Gemini:ApiKey"]
                  ?? throw new ArgumentNullException("Gemini:ApiKey",
                     "appsettings.json dosyasında 'Gemini:ApiKey' anahtarı bulunamadı!");
    }

    /// <summary>
    /// Kullanıcının C# kodunu ve Roslyn statik analiz çıktısını birleştirerek
    /// Groq API'ye gönderir. Dönen yanıtı parse edip UI'ın 3 kolonlu kart
    /// yapısına (SORUN / AÇIKLAMA / ÇÖZÜM) uygun GeminiAnalysisResult döndürür.
    /// Timeout, rate limit ve ağ hataları ayrı ayrı yakalanır; uygulama çökmez.
    /// </summary>
    public async Task<GeminiAnalysisResult> AnalyzeCodeAsync(string code, string roslynContext, CancellationToken cancellationToken = default)
    {
        try
        {
            // --- OPTİMİZE EDİLMİŞ SYSTEM PROMPT (Deniz) ---
            // System prompt ve user prompt Groq'un chat completion formatına ayrıldı.
            // System prompt: Modelin rolünü, çıktı formatını ve kısıtları tanımlar.
            // User prompt:   Analiz edilecek kodu ve Roslyn bağlamını içerir.
            // Bu ayrım sayesinde model "sohbet moduna" girmez, sadece JSON üretir.

            // (Deniz) System prompt: Groq'a kesin talimatlar verir.
            // "SADECE JSON" emri + markdown yasağı + halüsinasyon önleyici kısıtlar burada.
            const string systemPrompt =
                "Sen bir C# kod kalitesi analiz motorusun. " +
                "GÖREVİN: Sana verilen C# kodundaki kalite sorunlarını tespit etmek.\n\n" +
                "## KESİN KURALLAR (İhlal ETMEMELİSİN):\n" +
                "1. Yanıtın YALNIZCA aşağıdaki JSON şemasından oluşmalıdır. " +
                "Başında veya sonunda HİÇBİR metin, selamlama veya açıklama olmayacak.\n" +
                "2. Markdown kod blokları (```) KESİNLİKLE YASAKTIR. Sadece ham JSON yaz.\n" +
                "3. \"results\" dizisindeki her eleman tam olarak şu 4 alandan oluşmalıdır: " +
                "sorun, aciklama, cozum, severity.\n" +
                "4. severity değeri YALNIZCA \"High\", \"Medium\" veya \"Low\" olabilir.\n" +
                "5. Roslyn statik analiz ihlalleri varsa onları da analiz listene ekle.\n" +
                "6. Kodda hiç sorun yoksa results dizisini BOŞ bırak: {\"results\": []}\n" +
                "7. 'cozum' alanına yalnızca C# kodu veya somut adımlar yaz; " +
                "genel tavsiye veya felsefi yorum YAZMA.\n\n" +
                "## ZORUNLU JSON ŞEMASI:\n" +
                "{\"results\": [{\"sorun\": \"...\", \"aciklama\": \"...\", \"cozum\": \"...\", \"severity\": \"High|Medium|Low\"}]}";

            // (Deniz) User prompt: Büyük dosyalarda hallüsinasyonu azaltmak için
            // Roslyn bağlamı ve kod net başlıklar ile ayrıldı.
            // Kodun satır sayısını da veriyoruz ki model "kaç satır" göreceğini bilsin.
            int lineCount = code.Split('\n').Length;
            string userPrompt =
                $"## ROSLYN STATİK ANALİZ SONUÇLARI ({(roslynContext.Contains("ihlal bulunamadı") ? "Temiz" : "İhlaller Mevcut")})\n" +
                $"{roslynContext}\n\n" +
                $"## ANALİZ EDİLECEK C# KODU ({lineCount} satır)\n" +
                $"```csharp\n{code}\n```\n\n" +
                "Yukarıdaki kodu analiz et ve SADECE JSON formatında yanıt ver.";

            // (Deniz) Groq'un Chat Completion API'si "messages" dizisi bekler:
            // system mesajı → modelin genel davranışını belirler
            // user mesajı   → analiz edilecek kod ve bağlam
            var requestBody = new
            {
                model = "llama3-8b-8192",   // (Deniz) Groq'un hızlı Llama3 modeli; gerekirse değiştir
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user",   content = userPrompt   }
                },
                temperature = 0.1,          // (Deniz) Düşük temperature = daha deterministik, az halüsinasyon
                max_tokens = 4096           // (Deniz) Büyük dosyalar için yeterli token limiti
            };

            // İstek gövdesini JSON'a serialize edip UTF-8 olarak hazırla
            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            // (Deniz) Groq API endpoint'i. Authorization header HttpClient'a DI sırasında
            // eklenmişse buraya tekrar eklemene gerek yok.
            // Eğer eklenmemişse aşağıdaki satırı aktif et:
            // _httpClient.DefaultRequestHeaders.Authorization =
            //     new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
            var url = "https://api.groq.com/openai/v1/chat/completions";

            // --- RETRY MEKANİZMASI İLE HTTP ÇAĞRISI (Deniz) ---
            // _retryPolicy.ExecuteAsync() mevcut PostAsync çağrısını sarar (wrap eder).
            // 429 veya 5xx alınırsa policy devreye girerek belirtilen süreler sonra tekrar dener.
            // Başarılı olursa response doğrudan döner; 3 denemede başarısız olursa son hatayı fırlatır.
            HttpResponseMessage response;
            try
            {
                response = await _retryPolicy.ExecuteAsync(
                    async ct => await _httpClient.PostAsync(url, content, ct),
                    cancellationToken);
            }
            catch (Exception retryEx)
            {
                // (Deniz) 3 retry sonunda hâlâ hata varsa buraya düşer; uygulama çökmez.
                return GeminiAnalysisResult.Failure(
                    $"Groq API'ye 3 deneme sonunda ulaşılamadı. Son hata: {retryEx.Message}");
            }

            // HTTP 429 (Too Many Requests) → 3 retry sonunda da limit aşıldıysa
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
                return GeminiAnalysisResult.Failure(
                    "Groq API istek limiti (rate limit) 3 denemede de aşıldı. Lütfen birkaç dakika bekleyip tekrar deneyin.");

            // Diğer başarısız HTTP kodları için (4xx, 5xx) açıklayıcı mesaj döndür
            if (!response.IsSuccessStatusCode)
                return GeminiAnalysisResult.Failure(
                    $"Groq API isteği başarısız. HTTP Durum Kodu: {(int)response.StatusCode} {response.StatusCode}");

            // Yanıt gövdesini string olarak oku (yine asenkron, UI donmaz)
            var jsonStr = await response.Content.ReadAsStringAsync(cancellationToken);

            // (Deniz) Groq Chat Completion yanıt formatı:
            // choices[0] > message > content
            // (Gemini'nin candidates[0] > content > parts[0] > text formatından farklı!)
            using var jsonDocument = JsonDocument.Parse(jsonStr);
            var rawText = jsonDocument.RootElement
                              .GetProperty("choices")[0]
                              .GetProperty("message")
                              .GetProperty("content")
                              .GetString() ?? string.Empty;

            // --- MARKDOWN TEMIZLEME ---
            // Bazen model talimata rağmen yanıtı ```json ... ``` bloğuna sarar.
            // Regex ile bu bloğu tespit edip içeriği çıkarıyoruz; parse etmeden önce zorunlu.
            var cleanedJson = TemizleMarkdownBloklari(rawText);

            // --- JSON PARSE VE DÖNÜŞÜM ---
            // Temizlenmiş JSON'u UI kartlarına dönüştür
            return ParseGeminiJsonToResult(cleanedJson);
        }
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            // CancellationToken iptal edilmediyse bu timeout'tan kaynaklanıyor demektir
            return GeminiAnalysisResult.Failure(
                "Groq API yanıt süresi doldu (timeout). İnternet bağlantınızı kontrol edin ve tekrar deneyin.");
        }
        catch (TaskCanceledException)
        {
            // Kullanıcı veya sistem analizi bilinçli olarak iptal etti
            return GeminiAnalysisResult.Failure("Analiz işlemi iptal edildi.");
        }
        catch (HttpRequestException ex)
        {
            // DNS çözümlenemedi, bağlantı reddedildi gibi ağ seviyesi hatalar
            return GeminiAnalysisResult.Failure(
                $"Ağ bağlantısı hatası: Groq API'ye ulaşılamadı. Detay: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Hiçbir kategoriye girmeyen beklenmedik hata; uygulama çökmez
            return GeminiAnalysisResult.Failure(
                $"Beklenmedik bir hata oluştu: {ex.Message}");
        }
    }

    /// <summary>
    /// Groq'un bazen yanıta eklediği Markdown bloklarını temizler.
    /// Regex ile blok tespit edilirse yalnızca iç içerik alınır.
    /// Blok yoksa ham metin olduğu gibi döner; her iki durumda da parse güvenlidir.
    /// </summary>
    private static string TemizleMarkdownBloklari(string rawText)
    {
        // (?:json)? → "```json" veya sadece "```" ile başlayan blokları yakalar
        // [\s\S]*?  → Blok içindeki her şeyi (satır sonu dahil) yakalar
        var match = Regex.Match(rawText, @"```(?:json)?\s*([\s\S]*?)\s*```");

        // Regex eşleşmesi başarılıysa sadece gruplanan iç içerik döner
        // Eşleşme yoksa orijinal metin döner (zaten temiz JSON olabilir)
        return match.Success ? match.Groups[1].Value.Trim() : rawText.Trim();
    }

    /// <summary>
    /// Temizlenmiş JSON metnini GeminiAnalysisResult nesnesine dönüştürür.
    /// JSON bozuksa veya beklenen alanlar yoksa uygulama çökmez;
    /// IsSuccess=false olan bir fallback nesnesi döner.
    /// </summary>
    private static GeminiAnalysisResult ParseGeminiJsonToResult(string cleanedJson)
    {
        try
        {
            // Büyük/küçük harf duyarlılığını kapat (sorun/Sorun/SORUN hepsini yakalar)
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // JSON'u anonim şablona değil, dahili bir yardımcı sınıfa parse et
            var parsed = JsonSerializer.Deserialize<GeminiRawResponse>(cleanedJson, options);

            // Parse başarılı ama "results" alanı boş/null gelmiş olabilir
            if (parsed?.Results == null || parsed.Results.Count == 0)
                return GeminiAnalysisResult.Failure(
                    "Groq analiz sonucu döndürdü ancak herhangi bir sorun tespit edilmedi.");

            // Her raw result item'ı UI kartına (AnalysisCardDto) dönüştür ve listeye ekle
            var cards = parsed.Results.Select(r => new AnalysisCardDto
            {
                Sorun    = r.Sorun    ?? string.Empty,
                Aciklama = r.Aciklama ?? string.Empty,
                Cozum    = r.Cozum    ?? string.Empty,
                Severity = r.Severity ?? "Low"
            }).ToList();

            // Başarılı sonuç: IsSuccess=true, kart listesi dolu
            return new GeminiAnalysisResult { IsSuccess = true, Cards = cards };
        }
        catch (JsonException)
        {
            // JSON formatı bozuksa (model yanlış bir şey döndürdüyse) uygulama çökmez
            return GeminiAnalysisResult.Failure(
                "Groq'tan gelen yanıt geçerli bir JSON formatında değil. Lütfen tekrar deneyin.");
        }
    }

    /// <summary>
    /// Gerçek API çağrısı yapmadan servisi test etmek için kullanılan Mock metodudur.
    /// Geliştirme ve birim test aşamasında API anahtarı veya ağ bağlantısı
    /// olmaksızın UI entegrasyonunu doğrulamak amacıyla çağrılabilir.
    /// </summary>
    public static GeminiAnalysisResult GetMockSonucu()
    {
        // Gerçek API'den geliyormuş gibi davranacak 2 örnek kart oluştur
        return new GeminiAnalysisResult
        {
            IsSuccess = true,
            Cards = new List<AnalysisCardDto>
            {
                new()
                {
                    // Mock Kart 1: Uzun metod ihlali örneği
                    Sorun    = "Uzun Metod (Long Method)",
                    Aciklama = "ProcessData metodu 80 satırdan uzun. " +
                               "Tek sorumluluk prensibini (SRP) ihlal ediyor.",
                    Cozum    = "Metodu daha küçük, tek işlevli metodlara bölün. " +
                               "Örn: ValidateInput(), TransformData(), SaveResult()",
                    Severity = "High"
                },
                new()
                {
                    // Mock Kart 2: Magic Number kullanımı örneği
                    Sorun    = "Sihirli Sayı (Magic Number)",
                    Aciklama = "Kodda '42' değeri doğrudan kullanılmış. " +
                               "Bu değerin ne anlama geldiği belirsiz.",
                    Cozum    = "private const int MaxRetryCount = 42; şeklinde " +
                               "adlandırılmış sabit tanımlayın.",
                    Severity = "Medium"
                }
            }
        };
    }

    // Groq'un "results" dizisini parse etmek için kullanılan dahili yardımcı sınıf.
    // Bu sınıf dışarıya açılmaz (private); sadece ParseGeminiJsonToResult metodunda kullanılır.
    private class GeminiRawResponse
    {
        public List<GeminiRawCard> Results { get; set; } = new();
    }

    private class GeminiRawCard
    {
        public string? Sorun    { get; set; }
        public string? Aciklama { get; set; }
        public string? Cozum    { get; set; }
        public string? Severity { get; set; }
    }
}
