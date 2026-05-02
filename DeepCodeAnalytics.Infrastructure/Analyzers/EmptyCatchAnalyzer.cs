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
    /// Boş veya yalnızca yorum satırlarından oluşan Catch bloklarını tespit eder.
    /// Yutulan hatalar(swallowed exceptions) genellikle uygulamanın çökmesine engel olur gibi görünse de
    /// debug yapılmasını ve sorunun bulunmasını çok zorlaştırır.
    /// </summary>
    public class EmptyCatchAnalyzer : ICodeAnalyzer
    {
        public List<AnalysisDiagnostic> Analyze(SyntaxTree tree)
        {
            var diagnostics = new List<AnalysisDiagnostic>();
            var root = tree.GetRoot();

            // Ağaçtaki tüm catch bloklarını bul
            var catchClauses = root.DescendantNodes().OfType<CatchClauseSyntax>();

            foreach (var catchClause in catchClauses)
            {
                // Catch bloğunun içinde hiçbir C# ifadesi (statement) yoksa.
                // Not: Yorum satırları Roslyn'de "Trivia" olarak kabul edildiği için Statement sayılmazlar.
                // Dolayısıyla Statements.Count == 0 kontrolü, sadece yorum içeren catch'leri de boş sayar.
                if (catchClause.Block.Statements.Count == 0)
                {
                    var lineSpan = catchClause.GetLocation().GetLineSpan();
                    int lineNumber = lineSpan.StartLinePosition.Line + 1;

                    diagnostics.Add(new AnalysisDiagnostic(
                        title: "Boş Catch Bloğu (Empty Catch Block)",
                        severity: "High", // Hatanın yutulması kritik olabileceği için High olarak ayarlandı
                        line: lineNumber,
                        message: "Catch bloğu içinde hiçbir işlem yapılmıyor. Hata nesnesi yutuluyor ve loglanmıyor."
                    ));
                }
            }

            return diagnostics;
        }
    }
}
