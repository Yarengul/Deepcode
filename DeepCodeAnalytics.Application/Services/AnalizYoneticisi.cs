using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeepCodeAnalytics.Domain.Entities;
using DeepCodeAnalytics.Domain.Models;
using System.Text.Json;
using DeepCodeAnalytics.Application.Interfaces;

namespace DeepCodeAnalytics.Application.Services
{
    public class AnalizYoneticisi
    {
        private readonly AnalyzeService _analyzeService;
        private readonly IAiProviderFactory _aiFactory;
        private readonly IEmbeddingService _embeddingService;
        private readonly IVectorStore _vectorStore;

        // Yapıcı metot (Constructor)
        public AnalizYoneticisi(AnalyzeService analyzeService, IAiProviderFactory aiFactory, IEmbeddingService embeddingService, IVectorStore vectorStore)
        {
            _analyzeService = analyzeService;
            _aiFactory = aiFactory;
            _embeddingService = embeddingService;
            _vectorStore = vectorStore;
        }

        public async Task<AnalysisResult> AnalizEtAsync(string sourceCode, DeepCodeAnalytics.Application.Enums.AiEngineType engineType = DeepCodeAnalytics.Application.Enums.AiEngineType.Groq)
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

                // AI üzerinden öneri alır (Roslyn issue bulsun ya da bulmasın, AI her zaman çalışır)
                string roslynContext = report.Issues.Any()
                    ? JsonSerializer.Serialize(report.Issues)
                    : "[]";

                    // --- RAG (Retrieval-Augmented Generation) AŞAMASI ---
                    // 1. Kullanıcının kodunu vektöre çevir
                    float[] queryVector = await _embeddingService.GetEmbeddingAsync(sourceCode);
                    
                    // 2. Vektör veritabanında benzer kodları ara
                    var similarCodes = await _vectorStore.SearchSimilarAsync(queryVector, topK: 2);
                    
                    // 3. Bulunan örnekleri bağlama ekle
                    if (similarCodes.Any())
                    {
                        roslynContext += "\n\n### BENZER REFERANS KODLAR (En İyi Pratikler)\n";
                        roslynContext += "Aşağıdaki kodlar güvenlik veri setinden çekilmiş yüksek kaliteli C# çözüm örnekleridir:\n";
                        foreach (var sc in similarCodes)
                        {
                            roslynContext += $"- {sc.Description}\nÖrnek:\n{sc.CodeSnippet}\n\n";
                        }
                    }
                    // --- RAG AŞAMASI SONU ---
                    
                    var aiProvider = _aiFactory.GetProvider(engineType);
                    var aiResponse = await aiProvider.AnalyzeCodeAsync(sourceCode, roslynContext);

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
            catch (Exception ex)
            {
                Console.WriteLine($"Analiz Hatası: {ex.Message}");
                report.Issues.Add(new AnalysisIssue
                {
                    DiagnosticId = "RAG_HATA",
                    Message = $"AI veya Vektör Veritabanı Hatası: {ex.Message}",
                    Severity = "High",
                    Line = 0
                });
            }

            return report;
        }
    }
}