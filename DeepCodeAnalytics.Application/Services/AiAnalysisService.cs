using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Entities;
using DeepCodeAnalytics.Application.Services;
using System.Text;

namespace DeepCodeAnalytics.Application.Services;

/// <summary>
/// AI destekli kod analizi iş akışını yöneten Application Servisidir.
/// GeminiService, AnalyzeService (Roslyn) ve AnalysisRepository bileşenlerini bir araya getirerek
/// uçtan uca analiz sürecini orkestre eder.
/// </summary>
public class AiAnalysisService
{
    // Gemini API'ye istek atacak servis (Infrastructure'da implement edilir)
    private readonly IGeminiService _geminiService;

    // Analiz sonuçlarını veritabanına kaydedecek repository
    private readonly IAnalysisRepository _repository;

    // Nergis'in yazdığı statik analizleri çalıştıran merkezi servis
    private readonly AnalyzeService _analyzeService;

    // Constructor Injection ile bağımlılıklar dışarıdan verilir (DI Container)
    public AiAnalysisService(
        IGeminiService geminiService, 
        IAnalysisRepository repository,
        AnalyzeService analyzeService)
    {
        _geminiService = geminiService;
        _repository = repository;
        _analyzeService = analyzeService;
    }

    /// <summary>
    /// Verilen kaynak kodunu önce Roslyn ile analiz eder, çıkan ihlalleri string'e çevirip
    /// Gemini API'ye göndererek tam analiz akışını çalıştırır.
    /// Sonuç olarak veritabanına kaydedilmiş bir AnalysisResult döndürür.
    /// </summary>
    /// <param name="sourceCode">Kullanıcının analiz ettirdiği C# kaynak kodu</param>
    public async Task<AnalysisResult> PerformAnalysisAsync(string sourceCode, CancellationToken cancellationToken = default)
    {
        // ADIM 1: NERGİS'İN ROSLYN ANALİZİNİ ÇALIŞTIR
        var diagnostics = _analyzeService.Analyze(sourceCode);
        
        // ADIM 2: DIAGNOSTIC LİSTESİNİ STRING'E (PROMPT BAĞLAMINA) ÇEVİR
        var roslynContextBuilder = new StringBuilder();
        if (diagnostics.Any())
        {
            foreach (var diag in diagnostics)
            {
                roslynContextBuilder.AppendLine($"- [Satır {diag.Line}] ({diag.Severity}): {diag.Title} -> {diag.Message}");
            }
        }
        else
        {
            roslynContextBuilder.AppendLine("Roslyn statik analizinde herhangi bir kural ihlali bulunamadı.");
        }
        
        string roslynViolations = roslynContextBuilder.ToString();

        // ADIM 3: CEM'İN GEMİNİ API ÇAĞRISINI YAP (Roslyn bağlamıyla birlikte)
        var aiResponse = await _geminiService.AnalyzeCodeAsync(sourceCode, roslynViolations, cancellationToken);

        // Hata durumunda UI'a hata mesajını fırlat (Veritabanına bozuk kayıt atma)
        if (!aiResponse.IsSuccess)
        {
            throw new Exception(aiResponse.ErrorMessage);
        }

        // ADIM 4: DTO'lar Domain modellerine (Entity'lere) dönüştürülür
        var analysisResult = new AnalysisResult
        {
            OriginalCode = sourceCode,
            CreatedAt = DateTime.UtcNow,

            // Kartlardaki Sorun ve Açıklamaları AnalysisIssue'ya eşleştiriyoruz
            Issues = aiResponse.Cards.Select(c => new AnalysisIssue
            {
                Message = $"{c.Sorun} - {c.Aciklama}",
                Severity = c.Severity
            }).ToList(),

            // Kartlardaki Açıklama ve Çözümleri AiSuggestion'a eşleştiriyoruz
            Suggestions = aiResponse.Cards.Select(c => new AiSuggestion
            {
                SuggestionText = c.Aciklama,
                ProposedCode = c.Cozum
            }).ToList()
        };

        // ADIM 5: Tamamlanan analiz sonucu SQLite veritabanına kaydedilir
        await _repository.AddAsync(analysisResult, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return analysisResult;
    }
}
