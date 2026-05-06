using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Models;

namespace DeepCodeAnalytics.Infrastructure.Analyzers
{
    /// <summary>
    /// İsimlendirme standartlarını kontrol eder.
    /// - Metot isimlerinin büyük harfle başlaması (PascalCase).
    /// - Değişken isimlerinin tek harfli olmaması (i, j, k, x, y, z gibi standart döngü/koordinat değişkenleri hariç).
    /// </summary>
    public class NamingConventionAnalyzer : ICodeAnalyzer
    {
        // 'e' EventArgs için, 'c' char döngüleri için, '_' discard/kullanılmayan değişkenler için izin verildi (False Positive önlemi)
        private static readonly string[] AllowedSingleCharVariables = { "i", "j", "k", "x", "y", "z", "e", "c", "_" };

        public List<AnalysisDiagnostic> Analyze(SyntaxTree tree)
        {
            var diagnostics = new List<AnalysisDiagnostic>();
            var root = tree.GetRoot();

            // 1. Metot isimlendirmelerini kontrol et (PascalCase)
            var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            foreach (var method in methods)
            {
                string methodName = method.Identifier.Text;
                if (!string.IsNullOrEmpty(methodName) && char.IsLower(methodName[0]))
                {
                    int line = method.GetLocation().GetLineSpan().StartLinePosition.Line + 1;
                    diagnostics.Add(new AnalysisDiagnostic(
                        title: "İsimlendirme Hatası (Naming Convention)",
                        severity: "Medium",
                        line: line,
                        message: $"'{methodName}' metodu küçük harfle başlıyor. Metot isimleri PascalCase olmalıdır."
                    ));
                }
            }

            // 2. Yerel değişken isimlendirmelerini kontrol et (Tek harf)
            var variableDeclarators = root.DescendantNodes().OfType<VariableDeclaratorSyntax>();
            foreach (var variable in variableDeclarators)
            {
                string variableName = variable.Identifier.Text;
                if (!string.IsNullOrEmpty(variableName) && variableName.Length == 1)
                {
                    if (!AllowedSingleCharVariables.Contains(variableName))
                    {
                        int line = variable.GetLocation().GetLineSpan().StartLinePosition.Line + 1;
                        diagnostics.Add(new AnalysisDiagnostic(
                            title: "İsimlendirme Hatası (Naming Convention)",
                            severity: "Medium",
                            line: line,
                            message: $"'{variableName}' değişkeni tek harfli. Anlamlı ve açıklayıcı bir isim kullanılmalıdır (izin verilenler: {string.Join(", ", AllowedSingleCharVariables)})."
                        ));
                    }
                }
            }
            
            // Parametreleri de tek harfli kontrolüne dahil edelim
            var parameters = root.DescendantNodes().OfType<ParameterSyntax>();
            foreach (var param in parameters)
            {
                string paramName = param.Identifier.Text;
                if (!string.IsNullOrEmpty(paramName) && paramName.Length == 1)
                {
                    if (!AllowedSingleCharVariables.Contains(paramName))
                    {
                        int line = param.GetLocation().GetLineSpan().StartLinePosition.Line + 1;
                        diagnostics.Add(new AnalysisDiagnostic(
                            title: "İsimlendirme Hatası (Naming Convention)",
                            severity: "Medium",
                            line: line,
                            message: $"'{paramName}' parametresi tek harfli. Anlamlı ve açıklayıcı bir isim kullanılmalıdır."
                        ));
                    }
                }
            }

            return diagnostics;
        }
    }
}
