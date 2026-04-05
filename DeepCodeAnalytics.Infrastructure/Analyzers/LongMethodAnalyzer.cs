using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace DeepCodeAnalytics.Infrastructure.Analyzers
{
    /// <summary>
    /// Bir metot gövdesindeki ifade (statement) veya satır sayısının çok fazla olmasını engeller.
    /// Single Responsibility kuralını korumayı, metodu sade ve test edilebilir tutmayı amaçlar.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class LongMethodAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "SM001";
        private const string Title = "Çok Uzun Metot (Long Method)";
        private const string MessageFormat = "Metot '{0}' {1} satır(veya ifade) içeriyor. Bu değer önerilen {2} eşik değerinin üzerinde.";
        private const string Category = "CodeSmell";

        // Maksimum kabul edilebilir ifade sayısı.
        private const int MaxAllowedStatements = 20;

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
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            // Metot tanımlamalarını (Node) dinliyoruz
            context.RegisterSyntaxNodeAction(AnalyzeMethod, SyntaxKind.MethodDeclaration);
        }

        private void AnalyzeMethod(SyntaxNodeAnalysisContext context)
        {
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;

            // Eğer metodun içine yazılmış gövdesi (body) varsa
            if (methodDeclaration.Body != null)
            {
                // Toplam ifade (statement) sayısını C# ağacı üzerinden hesapla
                int statementCount = methodDeclaration.Body.Statements.Count;

                if (statementCount > MaxAllowedStatements)
                {
                    // Eğer sınır aşılmışsa diagnostic report fırlat
                    var diagnostic = Diagnostic.Create(
                        Rule, 
                        methodDeclaration.Identifier.GetLocation(), 
                        methodDeclaration.Identifier.Text, 
                        statementCount, 
                        MaxAllowedStatements);
                        
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
