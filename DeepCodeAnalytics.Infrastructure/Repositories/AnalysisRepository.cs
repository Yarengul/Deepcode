using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Entities;
using DeepCodeAnalytics.Infrastructure.Data;

namespace DeepCodeAnalytics.Infrastructure.Repositories;

/// <summary>
/// IAnalysisRepository arayüzünün EF Core + SQLite üzerinde çalışan somut implementasyonudur.
/// Application katmanı bu sınıfı doğrudan bilmez; sadece arayüzü (interface) üzerinden kullanır.
/// </summary>
public class AnalysisRepository : IAnalysisRepository
{
    // Veritabanı işlemlerini gerçekleştiren EF Core DbContext
    private readonly AppDbContext _context;

    // Constructor Injection: DbContext dışarıdan DI Container tarafından sağlanır
    public AnalysisRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Yeni bir analiz kaydını EF Core takip listesine ekler.
    /// Not: Bu metod henüz veritabanına YAZMAZ, sadece "eklenecek" olarak işaretler.
    /// Kayıt için SaveChangesAsync çağrılmalıdır.
    /// </summary>
    public async Task AddAsync(AnalysisResult result, CancellationToken cancellationToken = default)
    {
        await _context.AnalysisResults.AddAsync(result, cancellationToken);
    }

    /// <summary>
    /// EF Core'un takip ettiği tüm değişiklikleri veritabanına kalıcı olarak yazar.
    /// Bu metod çağrılmadan hiçbir veri fiziksel olarak kaydedilmez.
    /// </summary>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
