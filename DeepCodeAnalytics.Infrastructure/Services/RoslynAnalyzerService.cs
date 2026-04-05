using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Entities;
using DeepCodeAnalytics.Infrastructure.Analyzers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace DeepCodeAnalytics.Infrastructure.Services
{
    /// <summary>
    /// Kodu alıp Roslyn yapısına çeviren ve Diagnostic analyzer motorlarını çalıştıran servis sınıfı.
    /// Scrum Master'ın isimlendirme kurallarına göre RoslynAnalyzerService olarak adlandırılmıştır.
    /// </summary>
    public class RoslynAnalyzerService : ICodeAnalyzerService
    {
        public async Task<List<AnalysisIssue>> AnalyzeAsync(string sourceCode)
        {
            // 1. Yazı şeklindeki C# kodunu Syntax Tree (Sözdizimi ağacı) nesnesine çeviriyoruz.
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

            // 2. Güvenli bir ortamda derlenip Semantic Model elde edilebilmesi için bir Compilation nesnesi hazırlıyoruz.
            // .NET referanslarını koda dahil ediyoruz (mscorlib v.b. için System.Object kullanılabilir)
            var references = new[] {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
            };

            var compilation = CSharpCompilation.Create("DeepCodeAnalysisCompilation",
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            // 3. Çalıştırmak istediğimiz Scrum Master'ın onayladığı Analyzer listesini bağlıyoruz
            var analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(
                new LongMethodAnalyzer(),
                new EmptyCatchAnalyzer(),
                new MagicNumberAnalyzer()
            );

            // Compilation ağacına bu analiz kural setini tanıtıyoruz
            var compilationWithAnalyzers = compilation.WithAnalyzers(analyzers);

            // 4. Analizi bizzat tetikleyip tüm sorunları (Diagnostic) asenkron biçimde alıyoruz
            var diagnostics = await compilationWithAnalyzers.GetAllDiagnosticsAsync();

            var issueList = new List<AnalysisIssue>();

            // 5. Gelen tüm diagnostics nesnelerini kendi Domain modelimize (AnalysisIssue) dönüştürüp arayüz (UI) katmanına hazır hale getir
            foreach (var diagnostic in diagnostics)
            {
                // Sadece bizim bildiğimiz "SM" (Smell) ön ekiyle başlayan uyarıları listeye dahil et.
                if (diagnostic.Id.StartsWith("SM"))
                {
                    var lineSpan = diagnostic.Location.GetLineSpan();
                    
                    issueList.Add(new AnalysisIssue
                    {
                        DiagnosticId = diagnostic.Id,
                        Message = diagnostic.GetMessage(),
                        Severity = diagnostic.Severity.ToString(),
                        Line = lineSpan.StartLinePosition.Line + 1, // Satır numarası UI'da 1 bazlı gösterilmesi için +1 eklendi
                        Character = lineSpan.StartLinePosition.Character + 1
                    });
                }
            }

            return issueList;
        }
    }
}
