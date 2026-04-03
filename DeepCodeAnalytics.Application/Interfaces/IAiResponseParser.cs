using DeepCodeAnalytics.Application.DTOs;

namespace DeepCodeAnalytics.Application.Interfaces;

/// <summary>
/// Gemini API'den dönen ham JSON string'ini ayrıştırmakla sorumlu servis sözleşmesi.
/// Ayrıştırma mantığını soyutlar; farklı parser implementasyonlarına kapı açar.
/// </summary>
public interface IAiResponseParser
{
    /// <summary>
    /// Ham JSON yanıtını alır ve içindeki hataları (issues) ve
    /// önerileri (suggestions) parse ederek GeminiAnalysisDto'ya dönüştürür.
    /// </summary>
    /// <param name="jsonResponse">Gemini API'den gelen ham metin veya JSON string</param>
    GeminiAnalysisDto Parse(string jsonResponse);
}
