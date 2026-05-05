namespace DeepCodeAnalytics.Application.DTOs;

/// <summary>
/// Gemini AI servisinden Application ve UI katmanlarına döndürülen ana sonuç nesnesidir.
/// Başarı durumunu (IsSuccess), hata mesajını ve kart listesini bir arada taşır.
/// UI katmanı bu nesneye bakarak ne göstereceğine karar verir.
/// </summary>
public class GeminiAnalysisResult
{
    // API çağrısı veya JSON parse başarılı mı?
    // false gelirse UI kullanıcıya hata mesajı gösterir, kart listesi boş olur.
    public bool IsSuccess { get; set; } = true;

    // Hata durumunda kullanıcıya gösterilecek Türkçe hata açıklaması
    // (ör: "Ağ bağlantısı kurulamadı.", "API istek limiti aşıldı.")
    public string ErrorMessage { get; set; } = string.Empty;

    // Başarılı analizde UI'daki her karta karşılık gelen sonuç listesi
    public List<AnalysisCardDto> Cards { get; set; } = new();

    // Hata durumunda kullanılacak hazır fallback nesnesi üretir.
    // Uygulama çökmez; UI tarafına IsSuccess=false ve açıklayıcı mesaj iletilir.
    public static GeminiAnalysisResult Failure(string errorMessage) =>
        new() { IsSuccess = false, ErrorMessage = errorMessage, Cards = new() };
}

/// <summary>
/// UI'daki tek bir analiz kartını temsil eden DTO.
/// Her kart 3 kolona karşılık gelir:
///   SORUN    → Kırmızı kolon: Tespit edilen problem
///   AÇIKLAMA → Sarı kolon: Neden problem olduğunun açıklaması
///   ÇÖZÜM    → Yeşil kolon: AI'ın önerdiği düzeltme
/// </summary>
public class AnalysisCardDto
{
    // Kırmızı kolon: Tespit edilen kod problemi veya kural ihlali
    public string Sorun { get; set; } = string.Empty;

    // Sarı kolon: Problemin detaylı teknik açıklaması
    public string Aciklama { get; set; } = string.Empty;

    // Yeşil kolon: AI'ın önerdiği düzeltilmiş kod veya çözüm
    public string Cozum { get; set; } = string.Empty;

    // Hatanın önem seviyesi; UI renk kodlaması için kullanılır (High / Medium / Low)
    public string Severity { get; set; } = "Low";
}
