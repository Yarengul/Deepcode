using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;
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

        var analyzers = new List<ICodeAnalyzer>
        {
            new MagicNumberAnalyzer(),
            new EmptyCatchAnalyzer(),
            new LongMethodAnalyzer(),
            new NamingConventionAnalyzer()
        };

        var analyzeService = new AnalyzeService(analyzers);
        var groqService = new GroqService(new HttpClient(), configuration);
        var analizYoneticisi = new AnalizYoneticisi(analyzeService, groqService);

        ApplicationConfiguration.Initialize();
        System.Windows.Forms.Application.Run(new Form1(analizYoneticisi));
    }
}