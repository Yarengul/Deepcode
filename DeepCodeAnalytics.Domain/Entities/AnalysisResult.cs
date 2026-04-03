namespace DeepCodeAnalytics.Domain.Entities;

/// <summary>
/// Bir kod analizi oturumunun tüm sonuçlarını tutan ana Domain modelidir.
/// Analiz edilen orijinal kodu, oluşturulma tarihini,
/// tespit edilen hataları (Issues) ve AI önerilerini (Suggestions) içerir.
/// </summary>
public class AnalysisResult
{
    // Her analiz kaydını benzersiz şekilde tanımlayan GUID kimlik bilgisi
    public Guid Id { get; set; } = Guid.NewGuid();

    // Kullanıcının analiz ettirdiği orijinal kaynak kodu
    public string OriginalCode { get; set; } = default!;

    // Analizin yapıldığı tarih ve saat (UTC formatında saklanır)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Bu analize ait tespit edilen hata ve uyarı listesi (1'e-çok ilişki)
    public ICollection<AnalysisIssue> Issues { get; set; } = new List<AnalysisIssue>();

    // Bu analize ait AI tarafından üretilen öneri listesi (1'e-çok ilişki)
    public ICollection<AiSuggestion> Suggestions { get; set; } = new List<AiSuggestion>();
}
