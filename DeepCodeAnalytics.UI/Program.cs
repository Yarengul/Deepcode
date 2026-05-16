using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Application.Services;
using DeepCodeAnalytics.Infrastructure.Analyzers;
using DeepCodeAnalytics.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace DeepCodeAnalytics.UI;

internal static class Program
{
    [STAThread]
    /// <summary>
    /// Uygulama giriş noktası. Konfigürasyonu yükler, servisleri oluşturur ve Form'u başlatır.
    /// </summary>
    private static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // --- Sprint 4: Analyzer Test Kodu Başlangıcı ---
        // Uygulama her açıldığında uzun sürmemesi için yorum satırına alındı.
        // var runner = new AnalyzerTestRunner();
        // runner.RunTestsAndGenerateReportAsync().GetAwaiter().GetResult();
        // --- Sprint 4: Analyzer Test Kodu Bitişi ---

        var analyzers = new List<ICodeAnalyzer>
        {
            new MagicNumberAnalyzer(),
            new EmptyCatchAnalyzer(),
            new LongMethodAnalyzer(),
            new NamingConventionAnalyzer(),
            new SqlInjectionAnalyzer(),       // SQL Injection tespiti
            new HardcodedSecretAnalyzer()     // Hardcoded şifre/bağlantı tespiti
        };

        var analyzeService = new AnalyzeService(analyzers);
        var aiFactory = new AiProviderFactory(new HttpClient(), configuration);
        
        // RAG Servisleri — TF-IDF: API gerekmez, tfidf_vocab.json'dan yüklenir
        var embeddingService = new TfIdfEmbeddingService(configuration);
        var vectorStore = new LocalVectorStore(embeddingService);
        
        var analizYoneticisi = new AnalizYoneticisi(analyzeService, aiFactory, embeddingService, vectorStore);

        ApplicationConfiguration.Initialize();
        System.Windows.Forms.Application.Run(new Form1(analizYoneticisi));
    }
}