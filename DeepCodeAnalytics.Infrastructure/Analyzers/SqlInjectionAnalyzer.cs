using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Models;

namespace DeepCodeAnalytics.Infrastructure.Analyzers
{
    /// <summary>
    /// String birleştirme (concatenation) yoluyla oluşturulan SQL sorgularını tespit eder.
    /// Kullanıcı girdisi doğrudan SQL sorgusuna ekleniyorsa SQL Injection açığı oluşur.
    /// </summary>
    public class SqlInjectionAnalyzer : ICodeAnalyzer
    {
        private static readonly string[] SqlKeywords = { "SELECT", "INSERT", "UPDATE", "DELETE", "DROP", "EXEC", "WHERE" };

        public List<AnalysisDiagnostic> Analyze(SyntaxTree tree)
        {
            var diagnostics = new List<AnalysisDiagnostic>();
            var root = tree.GetRoot();

            // String birleştirme ifadelerini bul (+, +=)
            var binaryExpressions = root.DescendantNodes()
                .OfType<BinaryExpressionSyntax>()
                .Where(b => b.IsKind(SyntaxKind.AddExpression));

            foreach (var expr in binaryExpressions)
            {
                string exprText = expr.ToString();

                // SQL anahtar kelimesi içeren bir string birleştirmesi mi?
                bool hasSqlKeyword = SqlKeywords.Any(kw =>
                    exprText.Contains($"\"{kw} ", System.StringComparison.OrdinalIgnoreCase) ||
                    exprText.Contains($"'{kw} ", System.StringComparison.OrdinalIgnoreCase));

                if (!hasSqlKeyword) continue;

                // Değişken (identifier) veya metot çağrısı içeriyor mu? (kullanıcı girdisi riski)
                bool hasVariable = expr.DescendantNodes()
                    .Any(n => n is IdentifierNameSyntax || n is InvocationExpressionSyntax);

                if (hasVariable)
                {
                    var lineSpan = expr.GetLocation().GetLineSpan();
                    int lineNumber = lineSpan.StartLinePosition.Line + 1;

                    diagnostics.Add(new AnalysisDiagnostic(
                        title: "SQL Injection Açığı",
                        severity: "High",
                        line: lineNumber,
                        message: "SQL sorgusu string birleştirme (+) ile oluşturuluyor. Kullanıcı girdisi doğrudan sorguya ekleniyor. Parametreli sorgu (@param) kullanın."
                    ));
                }
            }

            return diagnostics;
        }
    }
}
