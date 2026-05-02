namespace DeepCodeAnalytics.Application.Interfaces;

/// <summary>
/// Google Gemini API ile iletişim kuracak servis için sözleşme (interface).
/// Clean Architecture gereği Infrastructure katmanı bu arayüzü uygular,
/// Application katmanı ise sadece bu arayüzü bilir (bağımlılık tersine çevrilir).
/// </summary>
public interface IGeminiService
{
    /// <summary>
    /// Verilen kaynak kodu ve Roslyn ihlallerini Gemini API'ye göndererek
    /// ham JSON yanıtını string olarak döndürür.
    /// </summary>
    /// <param name="code">Analiz edilecek C# kaynak kodu</param>
    /// <param name="roslynContext">Roslyn'den gelen statik analiz logları</param>
    /// <param name="cancellationToken">İptal tokeni</param>
    Task<string> AnalyzeCodeAsync(string code, string roslynContext, CancellationToken cancellationToken = default);
}
