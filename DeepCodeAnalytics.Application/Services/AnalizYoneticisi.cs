using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeepCodeAnalytics.Domain.Entities;
using DeepCodeAnalytics.Domain.Models;
using System.Text.Json;

namespace DeepCodeAnalytics.Application.Services
{
    public class AnalizYoneticisi
    {
        private readonly AnalyzeService _analyzeService;
        private readonly DeepCodeAnalytics.Application.Interfaces.IGeminiService _geminiService;

        // Yapıcı metot (Constructor)
        public AnalizYoneticisi(AnalyzeService analyzeService, DeepCodeAnalytics.Application.Interfaces.IGeminiService geminiService)
        {
            _analyzeService = analyzeService;
            _geminiService = geminiService;
        }

        public async Task<AnalysisResult> AnalizEtAsync(string sourceCode)
        {
            var report = new AnalysisResult { OriginalCode = sourceCode };

            try
            {
                // Kod analizi sürecini başlatır
                List<AnalysisDiagnostic> diagnostics = _analyzeService.Analyze(sourceCode);

                // Domain modeline map et (mevcut AnalysisResult.Issues tipi AnalysisIssue)
                foreach (var d in diagnostics)
                {
                    report.Issues.Add(new AnalysisIssue
                    {
                        DiagnosticId = d.Title ?? string.Empty,
                        Message = d.Message ?? string.Empty,
                        Severity = d.Severity ?? string.Empty,
                        Line = d.Line
                    });
                }

                // AI üzerinden öneri alır
                if (report.Issues.Any())
                {
                    // İhlalleri JSON string formatına çeviriyoruz ki Gemini için roslynContext oluşturabilelim
                    string roslynContext = JsonSerializer.Serialize(report.Issues);
                    
                    var aiResponse = await _geminiService.AnalyzeCodeAsync(sourceCode, roslynContext);

if (aiResponse.IsSuccess)
{
    var json = System.Text.Json.JsonSerializer.Serialize(aiResponse.Cards);
    report.Suggestions.Add(new AiSuggestion { SuggestionText = json });
}
else
{
    report.Suggestions.Add(new AiSuggestion { SuggestionText = aiResponse.ErrorMessage });
}
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda konsola bilgi yazdırır
                Console.WriteLine($"Analiz Hatası: {ex.Message}");
            }

            return report;
        }
    }
}