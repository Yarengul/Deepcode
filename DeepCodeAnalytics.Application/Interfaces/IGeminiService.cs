using DeepCodeAnalytics.Application.DTOs;

namespace DeepCodeAnalytics.Application.Interfaces;

/// <summary>
/// Google Gemini API ile iletişim kuracak servis için sözleşme (interface).
/// Clean Architecture gereği Infrastructure katmanı bu arayüzü uygular,
/// Application katmanı ise sadece bu arayüzü bilir (bağımlılık tersine çevrilir).
/// </summary>
public interface IGeminiService
{
    /// <summary>
    /// Verilen C# kaynak kodunu ve Roslyn ihlallerini Gemini API'ye gönderir.
    /// Dönen yanıtı parse ederek UI'ın 3 kolonlu kart yapısına uygun
    /// GeminiAnalysisResult nesnesini döndürür.
    /// Hata durumunda uygulama çökmez; IsSuccess=false olan bir nesne döner.
    /// </summary>
    /// <param name="code">Analiz edilecek C# kaynak kodu</param>
    /// <param name="roslynContext">Roslyn statik analiz çıktısı (ihlal logları)</param>
    /// <param name="cancellationToken">Asenkron işlemi iptal etmek için token</param>
    Task<GeminiAnalysisResult> AnalyzeCodeAsync(string code, string roslynContext, CancellationToken cancellationToken = default);
}
