using System.Text;
using System.Text.Json;
using DeepCodeAnalytics.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DeepCodeAnalytics.Infrastructure.Services;

/// <summary>
/// Google Gemini API ile HTTP üzerinden iletişim kuran Infrastructure servis sınıfıdır.
/// IGeminiService arayüzünü uygular; Application katmanı bu sınıfı doğrudan bilmez.
/// </summary>
public class GeminiService : IGeminiService
{
    // HTTP isteklerini yapmak için kullanılan client (DI Container tarafından yönetilir)
    private readonly HttpClient _httpClient;

    // appsettings.json veya konfigürasyondan okunan Gemini API anahtarı
    private readonly string _apiKey;

    // Constructor: HttpClient ve IConfiguration dışarıdan enjekte edilir
    public GeminiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        // API Key yoksa uygulama başlamadan hata fırlatılır (Fail-Fast prensibi)
        _apiKey = configuration["Gemini:ApiKey"] ?? throw new ArgumentNullException("Gemini API Anahtarı eksik!");
    }

    /// <summary>
    /// Kullanıcının C# kodunu ve Roslyn statik analiz loglarını birleştirerek
    /// zenginleştirilmiş bir prompt oluşturur ve Gemini API'ye gönderir.
    /// Gemini'nin ürettiği ham metin yanıtını string olarak döndürür.
    /// </summary>
    public async Task<string> AnalyzeCodeAsync(string code, string roslynContext, CancellationToken cancellationToken = default)
    {
        // --- ZENGİNLEŞTİRİLMİŞ PROMPT YAPISININ OLUŞTURULMASI ---
        // Prompt içinde hem Roslyn ihlaalleri hem de kullanıcı kodu yer alır.
        // Gemini'ye kesin bir JSON şeması verilerek tutarlı ve parse edilebilir
        // yanıt dönmesi sağlanır.
        var prompt = $@"Aşağıdaki C# kaynak kodunu ve Roslyn (statik kod analizi) loglarını incele.
Görev: Kod kalitesini artırmak için Clean Architecture prensiplerini ve SOLID kurallarını baz al.
Sadece JSON formatında cevap dön. JSON dışında herhangi bir metin veya ```json gibi markdown blokları içermesin. Schema şöyle olmalı:
{{
  ""issues"": [{{ ""message"": ""hata detayı"", ""severity"": ""High/Medium/Low"" }}],
  ""suggestions"": [{{ ""suggestionText"": ""neden önerildiği"", ""proposedCode"": ""yeni hali"" }}]
}}

### ROSLYN İHLALLERİ
{roslynContext}

### KULLANICI KODU
{code}";

        // Gemini API'nin beklediği istek gövdesi (contents > parts > text formatı)
        var requestBody = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = prompt } } }
            }
        };

        // İstek gövdesini JSON'a serialize edip UTF-8 olarak hazırla
        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        // API URL'sine anahtarı query string olarak ekle
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_apiKey}";

        // POST isteği at; başarısız HTTP kodu gelirse hata fırlat
        var response = await _httpClient.PostAsync(url, content, cancellationToken);
        response.EnsureSuccessStatusCode();

        // Yanıt gövdesini string olarak oku
        var jsonStr = await response.Content.ReadAsStringAsync(cancellationToken);
        
        // Gemini yanıt yapısının içinden yalnızca üretilen metin kısmını çıkar
        // Yanıt: candidates[0] > content > parts[0] > text
        using var jsonDocument = JsonDocument.Parse(jsonStr);
        var extractedText = jsonDocument.RootElement
                                .GetProperty("candidates")[0]
                                .GetProperty("content")
                                .GetProperty("parts")[0]
                                .GetProperty("text")
                                .GetString();

        return extractedText ?? string.Empty;
    }
}
