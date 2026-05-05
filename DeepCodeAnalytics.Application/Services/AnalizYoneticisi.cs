using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Bu satırlar interfaces ve domain klasörlerini projeye bağlar
using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Entities;
using System.Text.Json;

namespace DeepCodeAnalytics.Application.Services
{
    public class AnalizYoneticisi
    {
        // Interfaces klasöründeki dosya isimleriyle birebir aynı olmalı
        private readonly ICodeAnalyzerService _codeAnalyzer;
        private readonly IGeminiService _geminiService;

        // Yapıcı metot (Constructor)
        public AnalizYoneticisi(ICodeAnalyzerService codeAnalyzer, IGeminiService geminiService)
        {
            _codeAnalyzer = codeAnalyzer;
            _geminiService = geminiService;
        }

        public async Task<AnalysisResult> AnalizEtAsync(string sourceCode)
        {
            var report = new AnalysisResult { OriginalCode = sourceCode };

            try
            {
                // Kod analizi sürecini başlatır
                var issues = await _codeAnalyzer.AnalyzeAsync(sourceCode);
                foreach (var issue in issues) report.Issues.Add(issue);

                // AI üzerinden öneri alır
                if (report.Issues.Any())
                {
                    // İhlalleri JSON string formatına çeviriyoruz ki Gemini için roslynContext oluşturabilelim
                    string roslynContext = JsonSerializer.Serialize(report.Issues);
                    
                    var aiResponse = await _geminiService.AnalyzeCodeAsync(sourceCode, roslynContext);
                    
                    // Geçici olarak gelen raw stringi de Suggestions içine ekliyoruz
                    // UI tarafında veya burada parse edilebilir. GeminiService JSON string dönecek.
                    report.Suggestions.Add(new AiSuggestion { SuggestionText = aiResponse });
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