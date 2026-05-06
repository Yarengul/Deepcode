using System.Text.Json;
using System.Text.RegularExpressions;
using DeepCodeAnalytics.Application.DTOs;
using DeepCodeAnalytics.Application.Interfaces;

namespace DeepCodeAnalytics.Application.Parsers;

/// <summary>
/// Groq API'den dönen ham metni parse edip GeminiAnalysisDto'ya dönüştüren parser.
/// Sınıf adı UI uyumluluğu için korunmuştur; içerik Groq çıktısı için optimize edilmiştir.
/// </summary>
public class AiResponseParser : IAiResponseParser
{
    public GeminiAnalysisDto Parse(string rawResponse)
    {
        // (Deniz) 1. ADIM: Ham yanıtı önce temizle.
        // Bu metod; markdown blokları, sohbet önsözleri ve gereksiz whitespace'i siler.
        var cleanedJson = TemizleVeJsonCikart(rawResponse);

        // JSON ayrıştırmasında büyük/küçük harf duyarlılığını kapat
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        try
        {
            // (Deniz) 2. ADIM: Temizlenmiş metni deserialize et.
            // GeminiAnalysisDto doğrudan parse edilir (issues + suggestions formatı).
            return JsonSerializer.Deserialize<GeminiAnalysisDto>(cleanedJson, options)
                   ?? new GeminiAnalysisDto();
        }
        catch (JsonException)
        {
            // (Deniz) Ayrıştırma başarısızsa boş DTO dön; uygulama çökmez.
            // UI tarafa anlamlı bir mesaj iletilir.
            return new GeminiAnalysisDto
            {
                Issues = new List<IssueDto>
                {
                    new IssueDto
                    {
                        Message  = "AI yanıtı ayrıştırılamadı. Geçersiz JSON formatı.",
                        Severity = "Critical"
                    }
                }
            };
        }
    }

    /// <summary>
    /// Ham AI yanıtından temiz JSON metnini çıkaran yardımcı metod.
    /// Aşağıdaki tüm edge-case'leri ele alır:
    ///   - ```json ... ``` veya ``` ... ``` Markdown blokları
    ///   - "İşte kodunuzun analizi:" gibi sohbet önsözleri
    ///   - JSON öncesi veya sonrasındaki açıklama metinleri
    ///   - Başlangıç/bitiş whitespace karakterleri
    /// </summary>
    private static string TemizleVeJsonCikart(string rawText)
    {
        if (string.IsNullOrWhiteSpace(rawText))
            return "{}";

        // (Deniz) ADIM A: Markdown kod bloğunu temizle.
        // Groq bazen ```json ... ``` veya ``` ... ``` formatında yanıt verir.
        // Regex içindeki [\s\S]*? → satır sonu dahil her karakteri yakalar (non-greedy).
        var markdownMatch = Regex.Match(rawText, @"```(?:json)?\s*([\s\S]*?)\s*```");
        if (markdownMatch.Success)
        {
            // (Deniz) Markdown bloğu bulunduysa yalnızca iç içerik alınır.
            return markdownMatch.Groups[1].Value.Trim();
        }

        // (Deniz) ADIM B: Markdown bloğu yoksa sohbet önsözlerini sil.
        // Model bazen JSON'dan önce "İşte analiz sonuçlarım:" gibi metinler ekler.
        // '{' karakterinin ilk geçtiği konumdan itibaren metni al.
        // Hem { (nesne) hem de [ (dizi) başlangıç karakterlerini kontrol et.
        var jsonStartIndex = -1;
        for (int i = 0; i < rawText.Length; i++)
        {
            if (rawText[i] == '{' || rawText[i] == '[')
            {
                jsonStartIndex = i;
                break;
            }
        }

        if (jsonStartIndex > 0)
        {
            // (Deniz) JSON başlangıcından önce metin vardı; önsöz atılıyor.
            rawText = rawText[jsonStartIndex..];
        }

        // (Deniz) ADIM C: JSON sonrasındaki sohbet artıklarını da temizle.
        // Son '}' veya ']' karakterinin konumunu bul ve sonrasını at.
        var jsonEndIndex = -1;
        for (int i = rawText.Length - 1; i >= 0; i--)
        {
            if (rawText[i] == '}' || rawText[i] == ']')
            {
                jsonEndIndex = i;
                break;
            }
        }

        if (jsonEndIndex >= 0 && jsonEndIndex < rawText.Length - 1)
        {
            // (Deniz) JSON bittikten sonra fazladan metin vardı; kesiliyor.
            rawText = rawText[..(jsonEndIndex + 1)];
        }

        return rawText.Trim();
    }
}
