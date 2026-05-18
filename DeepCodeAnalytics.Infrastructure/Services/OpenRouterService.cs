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
                model = "deepseek/deepseek-v4-flash:free", // Confirmed working free model
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userPrompt }
                },
                temperature = 0.1
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

            var jsonStr = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
                return GeminiAnalysisResult.Failure($"OpenRouter API istek limiti aşıldı. Yanıt: {jsonStr}");

            if (!response.IsSuccessStatusCode)
            {
                return GeminiAnalysisResult.Failure($"OpenRouter API isteği başarısız. HTTP {(int)response.StatusCode}: {jsonStr}");
            }

            var rawText = "";
            try
            {
                using var jsonDocument = JsonDocument.Parse(jsonStr);

                // OpenRouter 200 OK dönmesine rağmen JSON içinde bir hata nesnesi barındırıyorsa
                if (jsonDocument.RootElement.TryGetProperty("error", out var errorProp))
                {
                    string errMsg = errorProp.TryGetProperty("message", out var msgProp) ? msgProp.GetString() ?? "" : "Bilinmeyen hata";
                    string errCode = errorProp.TryGetProperty("code", out var codeProp) ? codeProp.ToString() : "0";
                    return GeminiAnalysisResult.Failure($"OpenRouter Hata ({errCode}): {errMsg}");
                }

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
                return GeminiAnalysisResult.Failure($"Beklenmedik hata: {ex.Message} | Gelen JSON: {jsonStr}");
            }
        }
        catch (Exception ex)
        {
            return GeminiAnalysisResult.Failure($"API isteği sırasında hata oluştu: {ex.Message}");
        }
    }

    /// <summary>
    /// Gelen yanıttan JSON bloğunu çıkarır.
    /// DeepSeek R1 gibi modellerin ürettiği <think>...</think> düşünme bloklarını temizler.
    /// </summary>
    private static string TemizleMarkdownBloklari(string rawText)
    {
        if (string.IsNullOrWhiteSpace(rawText)) return "{}";

        // 0. <think> ... </think> düşünme bloğunu tamamen kaldır (DeepSeek R1 için kritik)
        rawText = Regex.Replace(rawText, @"<think>[\s\S]*?</think>", "", RegexOptions.IgnoreCase);

        // 1. Markdown ```json ... ``` bloğu
        var mdMatch = Regex.Match(rawText, @"```(?:json)?\s*([\s\S]*?)\s*```");
        if (mdMatch.Success) return mdMatch.Groups[1].Value.Trim();

        // 2. İlk { ile son } arasını al (en güvenli yöntem)
        int first = rawText.IndexOf('{');
        int last  = rawText.LastIndexOf('}');
        if (first >= 0 && last > first)
            return rawText[first..(last + 1)].Trim();

        return rawText.Trim();
    }

    private static string EscapeNewlinesInJsonStrings(string json)
    {
        if (string.IsNullOrEmpty(json)) return json;
        var sb = new StringBuilder();
        bool inString = false;
        bool escaped = false;
        for (int i = 0; i < json.Length; i++)
        {
            char c = json[i];
            if (c == '"' && !escaped)
            {
                inString = !inString;
                sb.Append(c);
            }
            else if (inString && (c == '\n' || c == '\r'))
            {
                sb.Append("\\n");
                if (c == '\r' && i + 1 < json.Length && json[i + 1] == '\n')
                {
                    i++; // Skip \n of \r\n
                }
            }
            else
            {
                sb.Append(c);
                if (inString)
                {
                    escaped = (c == '\\' && !escaped);
                }
                else
                {
                    escaped = false;
                }
            }
        }
        return sb.ToString();
    }

    private static GeminiAnalysisResult ParseGeminiJsonToResult(string cleanedJson)
    {
        try
        {
            cleanedJson = EscapeNewlinesInJsonStrings(cleanedJson);
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
