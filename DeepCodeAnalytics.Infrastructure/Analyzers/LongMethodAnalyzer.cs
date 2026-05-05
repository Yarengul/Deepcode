using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Models;

namespace DeepCodeAnalytics.Infrastructure.Analyzers
{
    /// <summary>
    /// Çok uzun metotları tespit eder.
    /// Gövdesi 30 satırdan fazla olan metotlar, okunabilirliği ve bakımı zorlaştırdığı için "High" severity ile raporlanır.
    /// </summary>
    public class LongMethodAnalyzer : ICodeAnalyzer
    {
        private const int MaxAllowedLines = 30;

        public List<AnalysisDiagnostic> Analyze(SyntaxTree tree)
        {
            var diagnostics = new List<AnalysisDiagnostic>();
            var root = tree.GetRoot();

            // Tüm metot tanımlarını bul
            var methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

            foreach (var method in methodDeclarations)
            {
                // Metodun bir gövdesi (süslü parantezli bloğu) varsa
                if (method.Body != null)
                {
                    // Gövdenin başlangıç ve bitiş satırlarını al
                    var lineSpan = method.Body.GetLocation().GetLineSpan();
                    int startLine = lineSpan.StartLinePosition.Line;
                    int endLine = lineSpan.EndLinePosition.Line;

                    // Satır sayısını hesapla (Süslü parantezlerin içi)
                    int lineCount = endLine - startLine - 1; 

                    if (lineCount > MaxAllowedLines)
                    {
                        int methodStartLine = method.GetLocation().GetLineSpan().StartLinePosition.Line + 1; // 1-based
                        string methodName = method.Identifier.Text;

                        diagnostics.Add(new AnalysisDiagnostic(
                            title: "Uzun Metot (Long Method)",
                            severity: "High",
                            line: methodStartLine,
                            message: $"'{methodName}' metodu {lineCount} satır uzunluğunda. Maksimum izin verilen sınır {MaxAllowedLines} satırdır. Metodu daha küçük parçalara bölmeyi düşünün."
                        ));
                    }
                }
            }

            return diagnostics;
        }
    }
}
