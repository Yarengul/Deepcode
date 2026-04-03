using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Linq;

namespace DeepCodeAnalytics.Infrastructure.Analyzers
{
    /// <summary>
    /// Koda doğrudan gömülmüş gizemli sayılar/değerler "Magic Number" denetimi yapar.
    /// Anlamlı bir değişkende/sabitte tutulmayan değerleri işaretler.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MagicNumberAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "SM003";
        private const string Title = "Sihirli Rakam (Magic Number)";
        private const string MessageFormat = "'{0}' değeri doğrudan kod içine yazılmış. Anlamlı bir isimlendirilmiş sabite (var/const) atanmalıdır.";
        private const string Category = "CodeSmell";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            DiagnosticId,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Info,
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            // Sayısal literal node'ları incelensin (NumericLiteralExpression)
            context.RegisterSyntaxNodeAction(AnalyzeNumericLiteral, SyntaxKind.NumericLiteralExpression);
        }

        private void AnalyzeNumericLiteral(SyntaxNodeAnalysisContext context)
        {
            var literalExpression = (LiteralExpressionSyntax)context.Node;
            
            // Eğer kabul edilebilecek temel index değerleri dense bypass et (0, 1, vb.)
            string valueText = literalExpression.Token.ValueText;
            if (valueText == "0" || valueText == "1" || valueText == "-1")
            {
                return;
            }

            // Bir Const/Field veya nesne deklare ediliyorsa güvenli bölgede
            var fieldDeclaration = literalExpression.Ancestors().OfType<FieldDeclarationSyntax>().FirstOrDefault();
            if (fieldDeclaration != null) return;
                
            var localDeclaration = literalExpression.Ancestors().OfType<LocalDeclarationStatementSyntax>().FirstOrDefault();
            if (localDeclaration != null) return;

            // Basit tutmak için geri kalan kod ortamlarındaki bağımsız literalları hata olarak değerlendir!
            var diagnostic = Diagnostic.Create(Rule, literalExpression.GetLocation(), literalExpression.Token.Text);
            context.ReportDiagnostic(diagnostic);
        }
    }
}
