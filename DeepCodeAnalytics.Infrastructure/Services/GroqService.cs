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
    /// Dönen içeriği aynı JSON şemasına parse eder.
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

KRİTİK TALİMATLAR:
1. Yanıtın SADECE ve YALNIZCA aşağıdaki JSON şemasında olmalıdır.
2. Başına veya sonuna hiçbir metin, açıklama, selamlama ekleme.
3. ```json gibi Markdown kod blokları KESİNLİKLE KULLANMA. Sadece ham JSON yaz.
4. ÇOK KRİTİK KURAL: JSON metin alanlarında (sorun, aciklama, cozum) KESİNLİKLE çift tırnak ("") kullanma! Kod isimlerini, değişkenleri, tırnak işaretlerini veya string ifadeleri vurgulamak için her zaman TEK TIRNAK ('') kullan. Çift tırnaklar JSON veri yapısını bozmaktadır.

JSON şeması:
{{
  ""results"": [
    {{
      ""sorun"": ""Tespit edilen kod problemi veya kural ihlalinin kısa özeti (Tek tırnak kullan)"",
      ""aciklama"": ""Problemin neden oluştuğunun teknik açıklaması (Örn: 'ApiKey' alanı - Tek tırnak kullan!)"",
      ""cozum"": ""Önerilen düzeltilmiş C# kodu veya çözüm adımları (C# kodundaki stringlerde ve her yerde çift tırnak yerine TEK TIRNAK kullan! Örn: string conn = 'connectionString')"",
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
                    new { role = "system", content = "You are a C# code security analyzer. Always respond with pure JSON only matching the schema. CRITICAL: Never use double quotes (\") inside any JSON string property values. Use single quotes (') for all inline quotes, variables, and code symbols." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.1
                // response_format kaldırıldı: json_validate_failed hatasına yol açıyor
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

            // Olası Markdown bloklarını temizle
            var cleanedJson = TemizleMarkdownBloklari(rawText);
            return ParseGroqJsonToResult(cleanedJson);
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

    /// <summary>
    /// Gelen yanıttan JSON bloğunu çıkarır.
    /// Markdown, prefix/suffix metinleri veya olası düşünme bloklarını temizler.
    /// </summary>
    private static string TemizleMarkdownBloklari(string rawText)
    {
        if (string.IsNullOrWhiteSpace(rawText)) return "{}";

        // 0. <think> ... </think> düşünme bloğunu tamamen kaldır
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

    /// <summary>
    /// AI yanıtındaki JSON'u UI kart DTO'larına dönüştürür.
    /// </summary>
    private static GeminiAnalysisResult ParseGroqJsonToResult(string cleanedJson)
    {
        try
        {
            cleanedJson = EscapeNewlinesInJsonStrings(cleanedJson);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var parsed = JsonSerializer.Deserialize<GroqRawResponse>(cleanedJson, options);

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
        catch (JsonException ex)
        {
            return GeminiAnalysisResult.Failure($"JSON Ayrıştırma Hatası: {ex.Message} | Ham Yanıt: {cleanedJson}");
        }
    }

    private sealed class GroqRawResponse
    {
        public List<GroqRawCard> Results { get; set; } = new();
    }

    private sealed class GroqRawCard
    {
        public string? Sorun { get; set; }
        public string? Aciklama { get; set; }
        public string? Cozum { get; set; }
        public string? Severity { get; set; }
    }
}

