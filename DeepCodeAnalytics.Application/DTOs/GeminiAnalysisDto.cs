namespace DeepCodeAnalytics.Application.DTOs;

/// <summary>
/// Gemini API'den gelen JSON yanıtını karşılamak için kullanılan
/// Veri Transfer Nesnesidir (DTO).
/// Domain modellerine dönüştürülmeden önce ham veriyi tutar.
/// </summary>
public class GeminiAnalysisDto
{
    // AI'ın tespit ettiği hata ve uyarıların listesi
    public List<IssueDto> Issues { get; set; } = new();

    // AI'ın önerdiği iyileştirmelerin listesi
    public List<SuggestionDto> Suggestions { get; set; } = new();
}

/// <summary>
/// Gemini API'den gelen tek bir hatayı (issue) temsil eden DTO.
/// </summary>
public class IssueDto
{
    // Hatanın açıklayıcı mesajı
    public string Message { get; set; } = default!;

    // Hatanın önem derecesi: High / Medium / Low
    public string Severity { get; set; } = default!;
}

/// <summary>
/// Gemini API'den gelen tek bir öneriyi (suggestion) temsil eden DTO.
/// </summary>
public class SuggestionDto
{
    // Önerinin neden yapıldığını açıklayan metin
    public string SuggestionText { get; set; } = default!;

    // AI tarafından önerilen düzeltilmiş kod bloğu
    public string ProposedCode { get; set; } = default!;
}
