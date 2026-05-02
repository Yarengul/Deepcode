using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using DeepCodeAnalytics.Domain.Models;

namespace DeepCodeAnalytics.Application.Interfaces
{
    /// <summary>
    /// Proje içerisindeki tüm analizörlerin (kuralların) uyması gereken ortak arayüz.
    /// Her analizör, kendisine verilen SyntaxTree üzerinde gezerek bulgularını AnalysisDiagnostic listesi olarak döner.
    /// </summary>
    public interface ICodeAnalyzer
    {
        /// <summary>
        /// Verilen C# kod ağacını (SyntaxTree) analiz eder.
        /// </summary>
        /// <param name="tree">Analiz edilecek Roslyn kod ağacı</param>
        /// <returns>Bulunan hataların/uyarıların listesi</returns>
        List<AnalysisDiagnostic> Analyze(SyntaxTree tree);
    }
}
