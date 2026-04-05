using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace DeepCodeAnalytics.UI
{
    internal static class Program
    {
        // Tüm uygulama boyunca kullanılacak bir Servis Sağlayıcı (IoC Container) referansı tutuyoruz.
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Uygulamanın ana giriş noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Windows Forms görsel ve DPI ayarları (Modern .NET standartları)
            System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.SystemAware);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            // Container oluşturulup bağımlılıklar ekleniyor
            var services = new ServiceCollection();
            ConfigureServices(services);

            // Ayarlar mühürleniyor ve Provider üretiliyor
            ServiceProvider = services.BuildServiceProvider();

            // Form sınıfını artık doğrudan "new Form1()" ile değil, Container üzerinden talep ediyoruz.
            // Bu sayede Form1'in istediği ICodeAnalyzerService otomatik olarak içerisine gönderilecektir.
            var mainForm = ServiceProvider.GetRequiredService<Form1>();
            
            System.Windows.Forms.Application.Run(mainForm);
        }

        /// <summary>
        /// Projedeki tüm interface ve somut sınıf eşleşmeleri burada yapılır.
        /// </summary>
        private static void ConfigureServices(IServiceCollection services)
        {
            // Form1 her istendiğinde yeni bir instance olarak verilsin.
            services.AddTransient<Form1>();

            // Ana iş kuralı motorumuzu bağlıyoruz. ICodeAnalyzerService isteyen yerlere RoslynAnalyzerService ver.
            services.AddScoped<ICodeAnalyzerService, RoslynAnalyzerService>();
        }
    }
}