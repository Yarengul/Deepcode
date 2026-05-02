using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Entities;

namespace DeepCodeAnalytics.Application.Services;

/// <summary>
/// AI destekli kod analizi iş akışını yöneten Application Servisidir.
/// GeminiService ve AnalysisRepository bileşenlerini bir araya getirerek
/// uçtan uca analiz sürecini orkestre eder.
/// </summary>
public class AiAnalysisService
{
    // Gemini API'ye istek atacak servis (Infrastructure'da implement edilir)
    private readonly IGeminiService _geminiService;

    // Analiz sonuçlarını veritabanına kaydedecek repository
    private readonly IAnalysisRepository _repository;

    // Constructor Injection ile bağımlılıklar dışarıdan verilir (DI Container)
    public AiAnalysisService(
        IGeminiService geminiService, 
        IAnalysisRepository repository)
    {
        _geminiService = geminiService;
        _repository = repository;
    }

    /// <summary>
    /// Verilen kaynak kodunu ve Roslyn ihlallerini kullanarak tam analiz akışını çalıştırır.
    /// Sonuç olarak veritabanına kaydedilmiş bir AnalysisResult döndürür.
    /// </summary>
    /// <param name="sourceCode">Kullanıcının analiz ettirdiği C# kaynak kodu</param>
    /// <param name="roslynViolations">Roslyn'den gelen statik analiz uyarı logları</param>
    public async Task<AnalysisResult> PerformAnalysisAsync(string sourceCode, string roslynViolations, CancellationToken cancellationToken = default)
    {
        // ADIM 1: Zenginleştirilmiş prompt oluşturulup Gemini API'ye gönderilir.
        // Artık parsing işlemi GeminiService içinde güvenli şekilde yapılıp bize DTO dönüyor.
        var aiResponse = await _geminiService.AnalyzeCodeAsync(sourceCode, roslynViolations, cancellationToken);

        // Hata durumunda UI'a hata mesajını fırlat (Veritabanına bozuk kayıt atma)
        if (!aiResponse.IsSuccess)
        {
            throw new Exception(aiResponse.ErrorMessage);
        }

        // ADIM 2: DTO'lar Domain modellerine (Entity'lere) dönüştürülür
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

        // ADIM 3: Tamamlanan analiz sonucu SQLite veritabanına kaydedilir
        await _repository.AddAsync(analysisResult, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return analysisResult;
    }
}
