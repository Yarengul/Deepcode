namespace DeepCodeAnalytics.Domain.Entities;

/// <summary>
/// Bir analiz oturumunda Roslyn veya AI tarafından tespit edilen
/// tek bir hata ya da uyarıyı temsil eden Domain modelidir.
/// Her Issue, hangi analize ait olduğunu AnalysisResultId ile bilir.
/// </summary>
public class AnalysisIssue
{
    // Hatayı benzersiz şekilde tanımlayan GUID kimlik bilgisi
    public Guid Id { get; set; } = Guid.NewGuid();

    // Bu hatanın hangi analiz oturumuna ait olduğunu gösteren yabancı anahtar
    public Guid AnalysisResultId { get; set; }

    // Hatanın açıklayıcı mesajı (ör: "Değişken adı küçük harfle başlıyor")
    public string Message { get; set; } = default!;

    // Hatanın önem derecesi: High / Medium / Low
    public string Severity { get; set; } = default!;

    // EF Core için ilişkilendirme (AnalysisResult tablosuna Navigation Property)
    public AnalysisResult AnalysisResult { get; set; } = default!;
}
