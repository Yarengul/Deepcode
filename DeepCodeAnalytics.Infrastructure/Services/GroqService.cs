using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using DeepCodeAnalytics.Application.DTOs;
using DeepCodeAnalytics.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DeepCodeAnalytics.Infrastructure.Services;

public class GroqService : IGeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    /// <summary>
    /// Groq servisi; HttpClient ve IConfiguration ile oluşturulur.
    /// ApiKey appsettings.json içindeki "Groq:ApiKey" alanından okunur.
    /// </summary>
    public GroqService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        // NOTE: API key should be stored under Groq:ApiKey in appsettings.json
        _apiKey = configuration["Groq:ApiKey"]
                  ?? throw new ArgumentNullException("Groq:ApiKey",
                      "appsettings.json dosyasında 'Groq:ApiKey' anahtarı bulunamadı!");
    }

    /// <summary>
    /// Verilen C# kodu ve Roslyn çıktısını Groq (OpenAI uyumlu) endpoint'ine gönderir.
    /// Dönen içeriği GeminiService ile aynı JSON şemasına parse eder.
    /// </summary>
    public async Task<GeminiAnalysisResult> AnalyzeCodeAsync(string code, string roslynContext, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_apiKey))
                return GeminiAnalysisResult.Failure("Groq API anahtarı bulunamadı. appsettings.json içinde 'Groq:ApiKey' tanımlayın.");

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

            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.2
            };

            // OpenAI uyumlu chat/completions isteği
            using var req = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions");
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            req.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(req, cancellationToken);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
                return GeminiAnalysisResult.Failure("Groq API istek limiti (rate limit) aşıldı. Lütfen birkaç saniye bekleyip tekrar deneyin.");

            if (!response.IsSuccessStatusCode)
            {
                // Hata ayıklama: HTTP kodu + body'nin tamamı
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
                return GeminiAnalysisResult.Failure($"HTTP {(int)response.StatusCode}: {errorBody}");
            }

            var jsonStr = await response.Content.ReadAsStringAsync(cancellationToken);
            using var jsonDocument = JsonDocument.Parse(jsonStr);

            var rawText = jsonDocument.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? string.Empty;

            // GeminiService ile aynı şekilde olası Markdown bloklarını temizle
            var cleanedJson = TemizleMarkdownBloklari(rawText);
            return ParseGeminiJsonToResult(cleanedJson);
        }
        catch (TaskCanceledException ex) when (!ex.CancellationToken.IsCancellationRequested)
        {
            return GeminiAnalysisResult.Failure("Groq API yanıt süresi doldu (timeout). İnternet bağlantınızı kontrol edin ve tekrar deneyin.");
        }
        catch (TaskCanceledException)
        {
            return GeminiAnalysisResult.Failure("Analiz işlemi iptal edildi.");
        }
        catch (HttpRequestException ex)
        {
            return GeminiAnalysisResult.Failure($"Ağ bağlantısı hatası: Groq API'ye ulaşılamadı. Detay: {ex.Message}");
        }
        catch (Exception ex)
        {
            return GeminiAnalysisResult.Failure($"Beklenmedik bir hata oluştu: {ex.Message}");
        }
    }

    private static string TemizleMarkdownBloklari(string rawText)
    {
        var match = Regex.Match(rawText, @"```(?:json)?\s*([\s\S]*?)\s*```");
        return match.Success ? match.Groups[1].Value.Trim() : rawText.Trim();
    }

    /// <summary>
    /// AI yanıtındaki JSON'u UI kart DTO'larına dönüştürür.
    /// </summary>
    private static GeminiAnalysisResult ParseGeminiJsonToResult(string cleanedJson)
    {
        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var parsed = JsonSerializer.Deserialize<GeminiRawResponse>(cleanedJson, options);

            if (parsed?.Results == null || parsed.Results.Count == 0)
                return GeminiAnalysisResult.Failure("AI analiz sonucu döndürdü ancak herhangi bir sorun tespit edilmedi.");

            var cards = parsed.Results.Select(r => new AnalysisCardDto
            {
                Sorun = r.Sorun ?? string.Empty,
                Aciklama = r.Aciklama ?? string.Empty,
                Cozum = r.Cozum ?? string.Empty,
                Severity = r.Severity ?? "Low"
            }).ToList();

            return new GeminiAnalysisResult { IsSuccess = true, Cards = cards };
        }
        catch (JsonException)
        {
            return GeminiAnalysisResult.Failure("AI'dan gelen yanıt geçerli bir JSON formatında değil. Lütfen tekrar deneyin.");
        }
    }

    private sealed class GeminiRawResponse
    {
        public List<GeminiRawCard> Results { get; set; } = new();
    }

    private sealed class GeminiRawCard
    {
        public string? Sorun { get; set; }
        public string? Aciklama { get; set; }
        public string? Cozum { get; set; }
        public string? Severity { get; set; }
    }
}

