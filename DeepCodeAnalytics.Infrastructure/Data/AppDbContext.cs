using DeepCodeAnalytics.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeepCodeAnalytics.Infrastructure.Data;

/// <summary>
/// Uygulamanın SQLite veritabanı bağlantısını ve tablo yapısını yöneten EF Core DbContext sınıfı.
/// Tüm Entity'leri (AnalysisResult, AnalysisIssue, AiSuggestion) bu Context üzerinden yönetir.
/// </summary>
public class AppDbContext : DbContext
{
    // DbContextOptions üzerinden bağlantı dizesi ve ayarlar dışarıdan enjekte edilir
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Veritabanındaki "AnalysisResults" tablosunu temsil eden DbSet
    public DbSet<AnalysisResult> AnalysisResults { get; set; } = default!;

    // Veritabanındaki "AnalysisIssues" tablosunu temsil eden DbSet
    public DbSet<AnalysisIssue> AnalysisIssues { get; set; } = default!;

    // Veritabanındaki "AiSuggestions" tablosunu temsil eden DbSet
    public DbSet<AiSuggestion> AiSuggestions { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // AnalysisResult ile AnalysisIssue arasında 1'e-çok ilişki kurulur.
        // Bir analiz silindiğinde ona ait tüm hatalar da otomatik silinir (Cascade).
        modelBuilder.Entity<AnalysisResult>()
            .HasMany(r => r.Issues)
            .WithOne(i => i.AnalysisResult)
            .HasForeignKey(i => i.AnalysisResultId)
            .OnDelete(DeleteBehavior.Cascade);

        // AnalysisResult ile AiSuggestion arasında 1'e-çok ilişki kurulur.
        // Bir analiz silindiğinde ona ait tüm öneriler de otomatik silinir (Cascade).
        modelBuilder.Entity<AnalysisResult>()
            .HasMany(r => r.Suggestions)
            .WithOne(s => s.AnalysisResult)
            .HasForeignKey(s => s.AnalysisResultId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
