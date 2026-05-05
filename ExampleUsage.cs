using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Application.Parsers;
using DeepCodeAnalytics.Application.Services;
using DeepCodeAnalytics.Infrastructure.Data;
using DeepCodeAnalytics.Infrastructure.Repositories;
using DeepCodeAnalytics.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeepCodeAnalytics.Example;

public class Program
{
    public static async Task Main(string[] args)
    {
        // 1. Dependency Injection Kurulumu
        var services = new ServiceCollection();
        
        // Konfigürasyon Kurulumu (App settings vs.)
        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddInMemoryCollection(new Dictionary<string, string?>
        {
            {"Gemini:ApiKey", "BURAYA_API_KEY_GELECEK"}
        });
        IConfiguration configuration = configBuilder.Build();
        services.AddSingleton(configuration);

        // HttpClient ve Gemini Service
        services.AddHttpClient<IGeminiService, GeminiService>();
        
        // App Services
        services.AddScoped<IAiResponseParser, AiResponseParser>();
        services.AddScoped<AiAnalysisService>();
        
        // Roslyn Analyzers (Nergis'in kodları)
        services.AddTransient<ICodeAnalyzer, DeepCodeAnalytics.Infrastructure.Analyzers.MagicNumberAnalyzer>();
        services.AddTransient<ICodeAnalyzer, DeepCodeAnalytics.Infrastructure.Analyzers.EmptyCatchAnalyzer>();
        services.AddTransient<ICodeAnalyzer, DeepCodeAnalytics.Infrastructure.Analyzers.LongMethodAnalyzer>();
        services.AddTransient<ICodeAnalyzer, DeepCodeAnalytics.Infrastructure.Analyzers.NamingConventionAnalyzer>();
        services.AddScoped<AnalyzeService>();
        
        // Entity Framework ve DbContext (SQLite için)
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=deepcode.db"));
            
        // Repository
        services.AddScoped<IAnalysisRepository, AnalysisRepository>();

        var serviceProvider = services.BuildServiceProvider();

        // 2. Veritabanı Migration Yaratma (Örnek olarak)
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.EnsureCreated(); // Migration olmadan tablo oluşturur
        }

        // 3. Kullanım
        var aiAnalysisService = serviceProvider.GetRequiredService<AiAnalysisService>();

        string sampleUserCode = @"
public class TestClass 
{
    public void doSomething() {
        int x = 5;
        Console.WriteLine(x);
    }
}";

        Console.WriteLine("AI Analysis in progress...");
        
        try 
        {
            var analysisResult = await aiAnalysisService.PerformAnalysisAsync(sampleUserCode);

            Console.WriteLine("Analysis Success!");
            Console.WriteLine("--- ISSUES ---");
            foreach (var issue in analysisResult.Issues)
            {
                Console.WriteLine($"- [{issue.Severity}] {issue.Message}");
            }

            Console.WriteLine("\n--- SUGGESTIONS ---");
            foreach (var suggestion in analysisResult.Suggestions)
            {
                Console.WriteLine($"- {suggestion.SuggestionText}");
                Console.WriteLine($"Proposed Code:\n{suggestion.ProposedCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }
    }
}
