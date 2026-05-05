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
        [STAThread]
        static void Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            var httpClient = new HttpClient();
            var geminiService = new GeminiService(httpClient, configuration);
            var roslynService = new RoslynAnalyzerService();

            var analizYoneticisi = new AnalizYoneticisi(roslynService, geminiService);

            ApplicationConfiguration.Initialize();
            
            System.Windows.Forms.Application.Run(new Form1(analizYoneticisi));
        }
    }
}