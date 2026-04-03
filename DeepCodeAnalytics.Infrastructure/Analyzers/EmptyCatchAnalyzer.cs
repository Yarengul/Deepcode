using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DeepCodeAnalytics.Infrastructure.Analyzers
{
    /// <summary>
    /// Boş veya yalnızca yorum satırlarından oluşan Catch bloklarını tespit eder.
    /// Yutulan hatalar(swallowed exceptions) genellikle uygulamanın çökmesine engel olur gibi görünse de
    /// debug yapılmasını ve sorunun bulunmasını çok zorlaştırır.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class EmptyCatchAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "SM002";
        private const string Title = "Boş Catch Bloğu (Empty Catch Block)";
        private const string MessageFormat = "Catch bloğu içinde hiçbir işlem yapılmıyor. Hata nesnesi yutuluyor.";
        private const string Category = "CodeSmell";

        // Kuralın tanıtımı
        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            DiagnosticId,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            // Derleyici tarafında üretilen dosyaları (örn. g.cs) dikkate alma.
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            
            // Analizi paralel işlemeye izni ver.
            context.EnableConcurrentExecution();

            // Catch satırını (Node) incelerken SyntaxKind.CatchClause kullanıyoruz.
            context.RegisterSyntaxNodeAction(AnalyzeSymbol, SyntaxKind.CatchClause);
        }

        private void AnalyzeSymbol(SyntaxNodeAnalysisContext context)
        {
            var catchClause = (CatchClauseSyntax)context.Node;

            // Catch bloğunun içinde hiçbir C# ifadesi (statement) yoksa. 
            // Varsa block tamamen boştur demektir (yorum satırları hariç).
            if (catchClause.Block.Statements.Count == 0)
            {
                var diagnostic = Diagnostic.Create(Rule, catchClause.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
