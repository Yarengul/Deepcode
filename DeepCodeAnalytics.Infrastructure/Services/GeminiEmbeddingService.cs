using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DeepCodeAnalytics.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Retry;

namespace DeepCodeAnalytics.Infrastructure.Services;

public class GeminiEmbeddingService : IEmbeddingService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    public GeminiEmbeddingService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Gemini:ApiKey"] 
                  ?? throw new ArgumentNullException("Gemini:ApiKey", "appsettings.json içinde Gemini API anahtarı bulunamadı.");
                  
        // Sadece gerçek ağ hataları için retry; 429 ana döngüde görünür geri sayımla yönetiliyor
        _retryPolicy = Policy
            .HandleResult<HttpResponseMessage>(r => r.StatusCode != HttpStatusCode.TooManyRequests && !r.IsSuccessStatusCode)
            .Or<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt * 2));
    }

    public async Task<float[]> GetEmbeddingAsync(string text, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Array.Empty<float>();

        // gemini-embedding-2: Bu key'in erişebildiği tek embedding modeli
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-embedding-2:embedContent?key={_apiKey}";

        var requestBody = new
        {
            model = "models/gemini-embedding-2",
            content = new
            {
                parts = new[] { new { text = text } }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await _retryPolicy.ExecuteAsync(
            async ct => await _httpClient.PostAsync(url, content, ct), 
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Embedding API hatası: {response.StatusCode} - {errorContent}");
        }

        var jsonStr = await response.Content.ReadAsStringAsync(cancellationToken);
        using var jsonDocument = JsonDocument.Parse(jsonStr);

        var valuesElement = jsonDocument.RootElement
                                        .GetProperty("embedding")
                                        .GetProperty("values");

        var floatArray = new float[valuesElement.GetArrayLength()];
        for (int i = 0; i < valuesElement.GetArrayLength(); i++)
        {
            floatArray[i] = (float)valuesElement[i].GetDouble();
        }

        return floatArray;
    }
}
