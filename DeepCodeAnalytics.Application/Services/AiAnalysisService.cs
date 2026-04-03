using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Entities;

namespace DeepCodeAnalytics.Application.Services;

/// <summary>
/// AI destekli kod analizi iş akışını yöneten Application Servisidir.
/// GeminiService, AiResponseParser ve AnalysisRepository bileşenlerini
/// bir araya getirerek uçtan uca analiz sürecini orkestre eder.
/// </summary>
public class AiAnalysisService
{
    // Gemini API'ye istek atacak servis (Infrastructure'da implement edilir)
    private readonly IGeminiService _geminiService;

    // API cevabını JSON'dan Domain modellerine parse edecek servis
    private readonly IAiResponseParser _responseParser;

    // Analiz sonuçlarını veritabanına kaydedecek repository
    private readonly IAnalysisRepository _repository;

    // Constructor Injection ile bağımlılıklar dışarıdan verilir (DI Container)
    public AiAnalysisService(
        IGeminiService geminiService, 
        IAiResponseParser responseParser, 
        IAnalysisRepository repository)
    {
        _geminiService = geminiService;
        _responseParser = responseParser;
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
        // ADIM 1: Zenginleştirilmiş prompt oluşturulup Gemini API'ye gönderilir
        // (Prompt içinde hem Roslyn logları hem de kullanıcı kodu bulunur)
        string aiRawResponse = await _geminiService.AnalyzeCodeAsync(sourceCode, roslynViolations, cancellationToken);

        // ADIM 2: Gelen ham JSON yanıtı DTO nesnelerine ayrıştırılır
        var parsedData = _responseParser.Parse(aiRawResponse);

        // ADIM 3: DTO'lar Domain modellerine (Entity'lere) dönüştürülür
        var analysisResult = new AnalysisResult
        {
            OriginalCode = sourceCode,
            CreatedAt = DateTime.UtcNow,

            // Her IssueDto → AnalysisIssue domain nesnesine maplenir
            Issues = parsedData.Issues.Select(i => new AnalysisIssue
            {
                Message = i.Message,
                Severity = i.Severity
            }).ToList(),

            // Her SuggestionDto → AiSuggestion domain nesnesine maplenir
            Suggestions = parsedData.Suggestions.Select(s => new AiSuggestion
            {
                SuggestionText = s.SuggestionText,
                ProposedCode = s.ProposedCode
            }).ToList()
        };

        // ADIM 4: Tamamlanan analiz sonucu SQLite veritabanına kaydedilir
        await _repository.AddAsync(analysisResult, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return analysisResult;
    }
}
