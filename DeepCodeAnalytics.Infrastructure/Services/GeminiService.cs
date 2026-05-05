using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using DeepCodeAnalytics.Application.DTOs;
using DeepCodeAnalytics.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DeepCodeAnalytics.Infrastructure.Services;

/// <summary>
/// Google Gemini API ile HTTP üzerinden iletişim kuran Infrastructure servis sınıfıdır.
/// IGeminiService arayüzünü uygular; Application katmanı bu sınıfı doğrudan bilmez.
/// Tüm HTTP hataları ve JSON parse hataları bu sınıf içinde yakalanır,
/// uygulama asla crash olmaz; hata bilgisi GeminiAnalysisResult.Failure() ile döner.
/// </summary>
public class GeminiService : IGeminiService
{
    // HTTP isteklerini yapmak için kullanılan client (DI Container tarafından yönetilir)
    private readonly HttpClient _httpClient;

    // appsettings.json dosyasındaki "Gemini:ApiKey" alanından okunan API anahtarı
    private readonly string _apiKey;

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
    /// Gemini API'ye gönderir. Dönen yanıtı parse edip UI'ın 3 kolonlu kart
    /// yapısına (SORUN / AÇIKLAMA / ÇÖZÜM) uygun GeminiAnalysisResult döndürür.
    /// Timeout, rate limit ve ağ hataları ayrı ayrı yakalanır; uygulama çökmez.
    /// </summary>
    public async Task<GeminiAnalysisResult> AnalyzeCodeAsync(string code, string roslynContext, CancellationToken cancellationToken = default)
    {
        try
        {
            // --- GÜÇLENDİRİLMİŞ PROMPT YAPISININ OLUŞTURULMASI ---
            // Prompt 3 bölümden oluşur:
            //   1) Görev tanımı ve kesin JSON emri
            //   2) Roslyn'den gelen statik analiz ihlalleri (bağlam zenginleştirme)
            //   3) Kullanıcının analiz ettirmek istediği C# kodu
            var prompt = $@"Sen bir C# kod kalitesi uzmanısın.
GÖREV: Aşağıdaki C# kaynak kodunu ve Roslyn statik analiz ihlallerini inceleyerek kod kalitesi sorunlarını tespit et.
Prensip: Clean Architecture ve SOLID kurallarını baz al.

KRİTİK TALİMAT: Yanıtın SADECE ve YALNIZCA aşağıdaki JSON şemasında olmalıdır.
Başına veya sonuna hiçbir metin, açıklama, selamlama ekleme.
```json gibi Markdown kod blokları KESİNLİKLE KULLANMA. Sadece ham JSON yaz.

JSON şeması:
{{
  ""results"": [
    {{
      ""sorun"": ""Tespit edilen kod problemi veya kural ihlalinin kısa özeti"",
      ""aciklama"": ""Problemin neden oluştuğunun teknik açıklaması"",
      ""cozum"": ""Önerilen düzeltilmiş C# kodu veya çözüm adımları"",
      ""severity"": ""High veya Medium veya Low""
    }}
  ]
}}

### ROSLYN STATİK ANALİZ İHLALLERİ (Bağlam)
{roslynContext}

### ANALİZ EDİLECEK C# KODU
{code}";

            // Gemini API'nin beklediği istek gövdesi: contents > parts > text formatı
            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            // İstek gövdesini JSON'a serialize edip UTF-8 olarak hazırla
            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            // API URL'sine anahtarı query string olarak ekle (Gemini v1beta endpoint)
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}";

            // Asenkron POST isteği at; await sayesinde UI thread bloklanmaz (donmaz)
            var response = await _httpClient.PostAsync(url, content, cancellationToken);

            // HTTP 429 (Too Many Requests) → Rate Limit aşıldı; özel mesaj döndür
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
                return GeminiAnalysisResult.Failure(
                    "Gemini API istek limiti (rate limit) aşıldı. Lütfen birkaç saniye bekleyip tekrar deneyin.");

            // Diğer başarısız HTTP kodları için (4xx, 5xx) açıklayıcı mesaj döndür
            if (!response.IsSuccessStatusCode)
                return GeminiAnalysisResult.Failure(
                    $"Gemini API isteği başarısız. HTTP Durum Kodu: {(int)response.StatusCode} {response.StatusCode}");

            // Yanıt gövdesini string olarak oku (yine asenkron, UI donmaz)
            var jsonStr = await response.Content.ReadAsStringAsync(cancellationToken);

            // Gemini'nin iç yanıt yapısından üretilen metin parçasını çıkar
            // Yanıt formatı: candidates[0] > content > parts[0] > text
            using var jsonDocument = JsonDocument.Parse(jsonStr);
            var rawText = jsonDocument.RootElement
                              .GetProperty("candidates")[0]
                              .GetProperty("content")
                              .GetProperty("parts")[0]
                              .GetProperty("text")
                              .GetString() ?? string.Empty;

            // --- MARKDOWN TEMIZLEME ---
            // Bazen Gemini talimata rağmen yanıtı ```json ... ``` bloğuna sarar.
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
                "Gemini API yanıt süresi doldu (timeout). İnternet bağlantınızı kontrol edin ve tekrar deneyin.");
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
                $"Ağ bağlantısı hatası: Gemini API'ye ulaşılamadı. Detay: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Hiçbir kategorialara girmeyen beklenmedik hata; uygulama çökmez
            return GeminiAnalysisResult.Failure(
                $"Beklenmedik bir hata oluştu: {ex.Message}");
        }
    }

    /// <summary>
    /// Gemini'nin bazen yanıta eklediği ```json ... ``` Markdown bloklarını temizler.
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
                    "Gemini analiz sonucu döndürdü ancak herhangi bir sorun tespit edilmedi.");

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
            // JSON formatı bozuksa (Gemini yanlış bir şey döndürdüyse) uygulama çökmez
            return GeminiAnalysisResult.Failure(
                "Gemini'den gelen yanıt geçerli bir JSON formatında değil. Lütfen tekrar deneyin.");
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

    // Gemini'nin "results" dizisini parse etmek için kullanılan dahili yardımcı sınıf.
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
