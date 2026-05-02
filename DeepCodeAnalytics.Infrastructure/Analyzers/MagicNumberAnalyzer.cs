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
    /// Koda doğrudan gömülmüş gizemli sayılar/değerler "Magic Number" denetimi yapar.
    /// Anlamlı bir değişkende/sabitte (const/readonly) tutulmayan değerleri işaretler.
    /// </summary>
    public class MagicNumberAnalyzer : ICodeAnalyzer
    {
        public List<AnalysisDiagnostic> Analyze(SyntaxTree tree)
        {
            var diagnostics = new List<AnalysisDiagnostic>();
            var root = tree.GetRoot();

            // Tüm sayısal literalleri (NumericLiteralExpression) bul
            var numericLiterals = root.DescendantNodes().OfType<LiteralExpressionSyntax>()
                .Where(n => n.IsKind(SyntaxKind.NumericLiteralExpression));

            foreach (var literal in numericLiterals)
            {
                string valueText = literal.Token.ValueText;

                // 0, 1 ve -1 gibi yaygın olarak kullanılan indeks/sayaç başlangıç değerlerini yoksay
                if (valueText == "0" || valueText == "1" || valueText == "-1")
                {
                    continue;
                }

                // Literal bir field tanımı içinde mi?
                var fieldDeclaration = literal.Ancestors().OfType<FieldDeclarationSyntax>().FirstOrDefault();
                if (fieldDeclaration != null)
                {
                    // Field tanımı const veya readonly ise magic number sayma
                    if (fieldDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.ConstKeyword) || m.IsKind(SyntaxKind.ReadOnlyKeyword)))
                    {
                        continue;
                    }
                }

                // Literal bir yerel değişken tanımı içinde mi?
                var localDeclaration = literal.Ancestors().OfType<LocalDeclarationStatementSyntax>().FirstOrDefault();
                if (localDeclaration != null)
                {
                    // Yerel değişken tanımı const ise magic number sayma
                    if (localDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.ConstKeyword)))
                    {
                        continue;
                    }
                }

                // Enum üyelerindeki atamaları yoksay (örnek: enum Status { Active = 2 })
                var enumMember = literal.Ancestors().OfType<EnumMemberDeclarationSyntax>().FirstOrDefault();
                if (enumMember != null)
                {
                    continue;
                }

                // Yukarıdaki filtrelerden geçemediyse, bu bir "Magic Number"dır.
                var lineSpan = literal.GetLocation().GetLineSpan();
                int lineNumber = lineSpan.StartLinePosition.Line + 1; // 0-based to 1-based

                diagnostics.Add(new AnalysisDiagnostic(
                    title: "Sihirli Rakam (Magic Number)",
                    severity: "Medium",
                    line: lineNumber,
                    message: $"'{valueText}' değeri doğrudan kod içine yazılmış. Anlamlı bir isimlendirilmiş sabite (const/readonly) atanmalıdır."
                ));
            }

            return diagnostics;
        }
    }
}
