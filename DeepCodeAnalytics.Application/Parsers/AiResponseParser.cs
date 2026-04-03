using System.Text.Json;
using System.Text.RegularExpressions;
using DeepCodeAnalytics.Application.DTOs;
using DeepCodeAnalytics.Application.Interfaces;

namespace DeepCodeAnalytics.Application.Parsers;

/// <summary>
/// Gemini API'den dönen ham metni JSON formatına dönüştüren parser sınıfı.
/// Bazen Gemini yanıtı "```json ... ```" gibi Markdown blokları içinde gelir;
/// bu sınıf önce o formati temizler, ardından deserializasyon yapar.
/// </summary>
public class AiResponseParser : IAiResponseParser
{
    public GeminiAnalysisDto Parse(string jsonResponse)
    {
        // Eğer yanıt ```json ... ``` formatında geldiyse sadece içeriği al
        // Regex ile Markdown kod bloğunu tespit et ve temizle
        var match = Regex.Match(jsonResponse, @"```(?:json)?\s*([\s\S]*?)\s*```");
        var jsonText = match.Success ? match.Groups[1].Value : jsonResponse;

        // JSON ayrıştırmasında büyük/küçük harf duyarlılığını kapat
        var options = new JsonSerializerOptions 
        { 
            PropertyNameCaseInsensitive = true 
        };

        try
        {
            // Temizlenen JSON string'ini GeminiAnalysisDto objesine dönüştür
            return JsonSerializer.Deserialize<GeminiAnalysisDto>(jsonText, options) 
                   ?? new GeminiAnalysisDto();
        }
        catch (JsonException)
        {
            // Ayrıştırma başarısızsa boş bir DTO dön ve hatayı issue olarak ekle
            // Böylece uygulama çökmez, UI tarafa anlamlı bir mesaj iletilir
            return new GeminiAnalysisDto 
            {
                Issues = new List<IssueDto> 
                { 
                    new IssueDto 
                    { 
                        Message = "AI yanıtı ayrıştırılamadı. Geçersiz JSON formatı.", 
                        Severity = "Critical" 
                    } 
                }
            };
        }
    }
}
