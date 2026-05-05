using DeepCodeAnalytics.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeepCodeAnalytics.Application.Interfaces
{
    /// <summary>
    /// Kod analizi işlemlerini soyutlayan servis arayüzü.
    /// Frontend/UI (Kullanıcı Arayüzü) katmanı bu arayüz üzerinden analiz motoruna (Infrastructure) erişir.
    /// Bu yaklaşım, Clean Architecture kurallarına uygun olarak sınıflar arası bağımlılıkları azaltır.
    /// </summary>
    public interface ICodeAnalyzerService
    {
        /// <summary>
        /// Verilen C# kaynak kodunu asenkron olarak analiz eder.
        /// Roslyn DiagnosticAnalyzer kurallarına göre çalıştırılarak bulunan hataları döndürür.
        /// </summary>
        /// <param name="sourceCode">Analiz edilecek ham C# kaynak kodu.</param>
        /// <returns>Tespit edilen ihlallerin (Code Smell/AnalysisIssue) listesi.</returns>
        Task<List<AnalysisIssue>> AnalyzeAsync(string sourceCode);
    }
}
