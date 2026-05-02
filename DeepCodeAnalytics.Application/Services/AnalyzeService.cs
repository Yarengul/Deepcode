using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Models;

namespace DeepCodeAnalytics.Application.Services
{
    /// <summary>
    /// Sistemde kayıtlı olan tüm kod analizörlerini tek bir noktadan çalıştıran servis sınıfı.
    /// UI veya API katmanı sadece bu servis ile konuşarak kod analizi yapar.
    /// </summary>
    public class AnalyzeService
    {
        private readonly IEnumerable<ICodeAnalyzer> _analyzers;

        /// <summary>
        /// AnalyzeService constructor'ı. Dependency Injection ile veya manuel olarak
        /// ICodeAnalyzer implementasyonlarını alır.
        /// </summary>
        /// <param name="analyzers">Sistemdeki analyzer nesneleri (MagicNumber, EmptyCatch vs.)</param>
        public AnalyzeService(IEnumerable<ICodeAnalyzer> analyzers)
        {
            _analyzers = analyzers;
        }

        /// <summary>
        /// Dışarıdan verilen string formatındaki C# kodunu Roslyn SyntaxTree'ye çevirip
        /// tüm kayıtlı analizörlerden geçirir.
        /// </summary>
        /// <param name="code">Analiz edilecek ham C# kodu</param>
        /// <returns>Tüm analizörlerden toplanan teşhis (diagnostic) listesi</returns>
        public List<AnalysisDiagnostic> Analyze(string code)
        {
            var results = new List<AnalysisDiagnostic>();

            // 1. Kodu Parse Et (SyntaxTree oluştur)
            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            // 2. Her bir analyzer'ı sırayla çalıştır ve dönen sonuçları listeye ekle
            foreach (var analyzer in _analyzers)
            {
                var diagnostics = analyzer.Analyze(syntaxTree);
                if (diagnostics != null && diagnostics.Any())
                {
                    results.AddRange(diagnostics);
                }
            }

            // 3. Sonuçları satır numarasına göre sırala (Okunabilirlik için)
            return results.OrderBy(r => r.Line).ToList();
        }
    }
}
