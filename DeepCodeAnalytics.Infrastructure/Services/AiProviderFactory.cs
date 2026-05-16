using System;
using System.Net.Http;
using DeepCodeAnalytics.Application.Enums;
using DeepCodeAnalytics.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DeepCodeAnalytics.Infrastructure.Services;

public class AiProviderFactory : IAiProviderFactory
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AiProviderFactory(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public IGeminiService GetProvider(AiEngineType engineType)
    {
        return engineType switch
        {
            AiEngineType.Gemini => new GeminiService(_httpClient, _configuration),
            AiEngineType.Groq => new GroqService(_httpClient, _configuration),
            AiEngineType.OpenRouter => new OpenRouterService(_httpClient, _configuration),
            _ => throw new ArgumentException("Bilinmeyen AI motoru", nameof(engineType))
        };
    }
}
