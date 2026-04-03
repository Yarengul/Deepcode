namespace DeepCodeAnalytics.Domain.Entities;

/// <summary>
/// Gemini AI'ın kod analizi sonucunda ürettiği tek bir öneriyi temsil eden Domain modelidir.
/// Her öneri; neden önerildiğini (SuggestionText) ve
/// düzeltilmiş kod bloğunu (ProposedCode) içerir.
/// </summary>
public class AiSuggestion
{
    // Öneriyi benzersiz şekilde tanımlayan GUID kimlik bilgisi
    public Guid Id { get; set; } = Guid.NewGuid();

    // Bu önerinin hangi analiz oturumuna ait olduğunu gösteren yabancı anahtar
    public Guid AnalysisResultId { get; set; }

    // AI'ın neden bu öneriyi yaptığını açıklayan metin
    // (ör: "Metod ismi büyük harfle başlamalıdır (PascalCase kuralı)")
    public string SuggestionText { get; set; } = default!;

    // AI'ın önerdiği düzeltilmiş kod parçası
    public string ProposedCode { get; set; } = default!;

    // EF Core için ilişkilendirme (AnalysisResult tablosuna Navigation Property)
    public AnalysisResult AnalysisResult { get; set; } = default!;
}
