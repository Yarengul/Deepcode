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
    /// Kaynak kodda açık yazılmış şifre, bağlantı dizesi ve API anahtarı gibi 
    /// hassas bilgileri tespit eder.
    /// </summary>
    public class HardcodedSecretAnalyzer : ICodeAnalyzer
    {
        private static readonly string[] DangerousKeywords =
        {
            "password", "passwd", "pwd", "secret", "apikey", "api_key",
            "connectionstring", "connectionStr", "token", "credentials"
        };

        public List<AnalysisDiagnostic> Analyze(SyntaxTree tree)
        {
            var diagnostics = new List<AnalysisDiagnostic>();
            var root = tree.GetRoot();

            // String literal atamalarını bul
            var assignments = root.DescendantNodes()
                .OfType<VariableDeclaratorSyntax>()
                .Where(v => v.Initializer?.Value is LiteralExpressionSyntax lit &&
                            lit.IsKind(SyntaxKind.StringLiteralExpression));

            foreach (var assignment in assignments)
            {
                string varName = assignment.Identifier.Text.ToLowerInvariant();
                string value = (assignment.Initializer?.Value as LiteralExpressionSyntax)?.Token.ValueText ?? "";

                bool isDangerous = DangerousKeywords.Any(kw => varName.Contains(kw));
                bool hasValue = value.Length > 3; // Boş veya çok kısa değerleri atla

                if (isDangerous && hasValue)
                {
                    var lineSpan = assignment.GetLocation().GetLineSpan();
                    int lineNumber = lineSpan.StartLinePosition.Line + 1;

                    diagnostics.Add(new AnalysisDiagnostic(
                        title: "Hardcoded Hassas Bilgi",
                        severity: "High",
                        line: lineNumber,
                        message: $"'{assignment.Identifier.Text}' değişkeni kaynak kodda sabit bir değer içeriyor. Şifre ve bağlantı bilgileri appsettings.json veya ortam değişkenlerinde saklanmalıdır."
                    ));
                }
            }

            // Field declaration'ları da kontrol et (class level)
            var fields = root.DescendantNodes()
                .OfType<FieldDeclarationSyntax>();

            foreach (var field in fields)
            {
                foreach (var variable in field.Declaration.Variables)
                {
                    string varName = variable.Identifier.Text.ToLowerInvariant();
                    if (variable.Initializer?.Value is not LiteralExpressionSyntax lit) continue;
                    if (!lit.IsKind(SyntaxKind.StringLiteralExpression)) continue;

                    string value = lit.Token.ValueText;
                    bool isDangerous = DangerousKeywords.Any(kw => varName.Contains(kw));

                    if (isDangerous && value.Length > 3)
                    {
                        var lineSpan = field.GetLocation().GetLineSpan();
                        int lineNumber = lineSpan.StartLinePosition.Line + 1;

                        diagnostics.Add(new AnalysisDiagnostic(
                            title: "Hardcoded Hassas Bilgi",
                            severity: "High",
                            line: lineNumber,
                            message: $"'{variable.Identifier.Text}' alanı kaynak kodda sabit bir değer içeriyor. Şifre ve bağlantı bilgileri appsettings.json veya ortam değişkenlerinde saklanmalıdır."
                        ));
                    }
                }
            }

            return diagnostics;
        }
    }
}
