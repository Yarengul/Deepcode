using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Application.Services;
using DeepCodeAnalytics.Infrastructure.Analyzers;

namespace DeepCodeAnalytics.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // --- Sprint 3: Analyzer Test Kodu Başlangıcı ---
            RunAnalyzerTests();
            // --- Sprint 3: Analyzer Test Kodu Bitişi ---

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            global::System.Windows.Forms.Application.Run(new Form1());
        }

        private static void RunAnalyzerTests()
        {
            // 1. Analyzer'ları oluştur (Manuel DI örneği)
            var analyzers = new List<ICodeAnalyzer>
            {
                new MagicNumberAnalyzer(),
                new EmptyCatchAnalyzer(),
                new LongMethodAnalyzer(),
                new NamingConventionAnalyzer()
            };

            // 2. Servisi oluştur
            var service = new AnalyzeService(analyzers);

            // 3. Test için hatalı C# kodu
            string testCode = @"
using System;

namespace TestApp
{
    public class TestClass
    {
        public void calculateSomething() // Naming (küçük harf)
        {
            int x = 10; // Naming (izin verilenlerden, hata vermez)
            int a = 5;  // Naming (tek harf)
            double pi = 3.14; // Magic number (const/readonly değil)
            
            try
            {
                int result = a * 2; // Magic number (2)
            }
            catch(Exception ex)
            {
                // Boş catch
            }
        }

        // Long method testi için uzun bir metot (Kısaltılmış gösterim)
        public void LongMethod()
        {
            int a1 = 1; int a2 = 1; int a3 = 1; int a4 = 1; int a5 = 1;
            int a6 = 1; int a7 = 1; int a8 = 1; int a9 = 1; int a10 = 1;
            int a11 = 1; int a12 = 1; int a13 = 1; int a14 = 1; int a15 = 1;
            int a16 = 1; int a17 = 1; int a18 = 1; int a19 = 1; int a20 = 1;
            int a21 = 1; int a22 = 1; int a23 = 1; int a24 = 1; int a25 = 1;
            int a26 = 1; int a27 = 1; int a28 = 1; int a29 = 1; int a30 = 1;
            int a31 = 1; int a32 = 1; int a33 = 1; int a34 = 1; int a35 = 1;
        }
    }
}
";

            // 4. Analizi çalıştır
            var diagnostics = service.Analyze(testCode);

            // 5. Sonuçları konsola / debug output'a yazdır
            Debug.WriteLine("=== DEEPCODE ANALYTICS TEST SONUÇLARI ===");
            foreach (var diag in diagnostics)
            {
                Debug.WriteLine($"[{diag.Severity}] Satır {diag.Line}: {diag.Title} - {diag.Message}");
            }
            Debug.WriteLine("=========================================");
        }
    }
}