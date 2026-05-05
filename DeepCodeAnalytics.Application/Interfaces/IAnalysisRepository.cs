using DeepCodeAnalytics.Domain.Entities;

namespace DeepCodeAnalytics.Application.Interfaces;

/// <summary>
/// Analiz verilerinin veritabanına yazılmasını soyutlayan repository sözleşmesi.
/// Application katmanı EF Core veya SQLite gibi detaylardan habersiz olur;
/// sadece bu arayüzü kullanır (Dependency Inversion Principle).
/// </summary>
public interface IAnalysisRepository
{
    /// <summary>
    /// Yeni bir analiz sonucunu veritabanına ekler (henüz kaydetmez).
    /// </summary>
    Task AddAsync(AnalysisResult result, CancellationToken cancellationToken = default);

    /// <summary>
    /// Bekleyen tüm değişiklikleri veritabanına kalıcı olarak kaydeder.
    /// </summary>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
