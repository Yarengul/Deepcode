using System;
using System.Net.Http;
using System.Windows.Forms;
using DeepCodeAnalytics.Application.Services;
using DeepCodeAnalytics.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

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
            // Yapılandırma dosyasını okuyoruz (appsettings.json)
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            // Gerekli servisleri (Dependency Injection benzeri) manuel ayağa kaldırıyoruz
            var httpClient = new HttpClient();
            var geminiService = new GeminiService(httpClient, configuration);
            var roslynService = new RoslynAnalyzerService();

            var analizYoneticisi = new AnalizYoneticisi(roslynService, geminiService);

            ApplicationConfiguration.Initialize();
            
            // Analiz yöneticisini forma enjekte ederek başlatıyoruz
            System.Windows.Forms.Application.Run(new Form1(analizYoneticisi));
        }
    }
}
