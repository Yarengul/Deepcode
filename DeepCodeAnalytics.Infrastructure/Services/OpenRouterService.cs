using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using DeepCodeAnalytics.Application.DTOs;
using DeepCodeAnalytics.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Retry;

namespace DeepCodeAnalytics.Infrastructure.Services;

public class OpenRouterService : IGeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    public OpenRouterService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenRouter:ApiKey"]
                  ?? throw new ArgumentNullException("OpenRouter:ApiKey", "appsettings.json dosyasında 'OpenRouter:ApiKey' anahtarı bulunamadı!");

        _retryPolicy = Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .Or<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    public async Task<GeminiAnalysisResult> AnalyzeCodeAsync(string code, string roslynContext, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(_apiKey))
                return GeminiAnalysisResult.Failure("OpenRouter API anahtarı bulunamadı. Lütfen kontrol edin.");

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
                "7. 'cozum' alanına yalnızca C# kodu veya somut adımlar yaz.\n\n" +
                "## ZORUNLU JSON ŞEMASI:\n" +
                "{\"results\": [{\"sorun\": \"...\", \"aciklama\": \"...\", \"cozum\": \"...\", \"severity\": \"High|Medium|Low\"}]}";

            int lineCount = code.Split('\n').Length;
            string userPrompt =
                $"## ROSLYN STATİK ANALİZ SONUÇLARI ({(roslynContext.Contains("ihlal bulunamadı") ? "Temiz" : "İhlaller Mevcut")})\n" +
                $"{roslynContext}\n\n" +
                $"## ANALİZ EDİLECEK C# KODU ({lineCount} satır)\n" +
                $"```csharp\n{code}\n```\n\n" +
                "Yukarıdaki kodu analiz et ve SADECE JSON formatında yanıt ver.";

            var requestBody = new
            {
                model = "deepseek/deepseek-v4-flash:free", // Sabit ve kararlı bir ücretsiz model kullanıyoruz
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userPrompt }
                },
                temperature = 0.1
                // response_format = new { type = "json_object" } // Kaldırıldı: Çoğu ücretsiz model bunu desteklemediği için hata fırlatıyor.
            };

            var url = "https://openrouter.ai/api/v1/chat/completions";

            HttpResponseMessage response;
            try
            {
                response = await _retryPolicy.ExecuteAsync(
                    async ct => 
                    {
                        var content = new StringContent(
                            JsonSerializer.Serialize(requestBody),
                            Encoding.UTF8,
                            "application/json");

                        using var req = new HttpRequestMessage(HttpMethod.Post, url);
                        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                        req.Headers.Add("HTTP-Referer", "http://localhost"); 
                        req.Headers.Add("X-Title", "DeepCode Analytics"); 
                        req.Content = content;

                        return await _httpClient.SendAsync(req, ct);
                    },
                    cancellationToken);
            }
            catch (Exception retryEx)
            {
                return GeminiAnalysisResult.Failure($"OpenRouter API'ye ulaşılamadı. Son hata: {retryEx.Message}");
            }

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
                return GeminiAnalysisResult.Failure("OpenRouter API istek limiti aşıldı.");

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                return GeminiAnalysisResult.Failure($"OpenRouter API isteği başarısız. HTTP {(int)response.StatusCode}: {errorBody}");
            }

            var rawText = "";
            var jsonStr = await response.Content.ReadAsStringAsync(cancellationToken);
            try
            {
                using var jsonDocument = JsonDocument.Parse(jsonStr);

                rawText = jsonDocument.RootElement
                                  .GetProperty("choices")[0]
                                  .GetProperty("message")
                                  .GetProperty("content")
                                  .GetString() ?? string.Empty;

                var cleanedJson = TemizleMarkdownBloklari(rawText);
                return ParseGeminiJsonToResult(cleanedJson);
            }
            catch (Exception ex)
            {
                return GeminiAnalysisResult.Failure($"Beklenmedik hata (KeyNotFound): {ex.Message} | Gelen JSON: {jsonStr}");
            }
        }
        catch (Exception ex)
        {
            return GeminiAnalysisResult.Failure($"API isteği sırasında hata oluştu: {ex.Message}");
        }
    }

    private static string TemizleMarkdownBloklari(string rawText)
    {
        var match = Regex.Match(rawText, @"```(?:json)?\s*([\s\S]*?)\s*```");
        return match.Success ? match.Groups[1].Value.Trim() : rawText.Trim();
    }

    private static GeminiAnalysisResult ParseGeminiJsonToResult(string cleanedJson)
    {
        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var parsed = JsonSerializer.Deserialize<GeminiRawResponse>(cleanedJson, options);

            if (parsed?.Results == null || parsed.Results.Count == 0)
                return GeminiAnalysisResult.Failure("OpenRouter analiz sonucu döndürdü ancak sorun tespit edilmedi.");

            var cards = parsed.Results.Select(r => new AnalysisCardDto
            {
                Sorun    = r.Sorun    ?? string.Empty,
                Aciklama = r.Aciklama ?? string.Empty,
                Cozum    = r.Cozum    ?? string.Empty,
                Severity = r.Severity ?? "Low"
            }).ToList();

            return new GeminiAnalysisResult { IsSuccess = true, Cards = cards };
        }
        catch (JsonException)
        {
            return GeminiAnalysisResult.Failure("OpenRouter'dan gelen yanıt geçerli bir JSON değil.");
        }
    }

    private class GeminiRawResponse
    {
        public List<GeminiRawCard> Results { get; set; } = new();
    }

    private class GeminiRawCard
    {
        public string? Sorun { get; set; }
        public string? Aciklama { get; set; }
        public string? Cozum { get; set; }
        public string? Severity { get; set; }
    }
}
