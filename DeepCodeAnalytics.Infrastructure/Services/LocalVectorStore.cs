using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Models;

namespace DeepCodeAnalytics.Infrastructure.Services;

public class LocalVectorStore : IVectorStore
{
    private readonly IEmbeddingService _embeddingService;
    private readonly string _databasePath;
    private List<CodeSearchItem> _dataset = new();

    public LocalVectorStore(IEmbeddingService embeddingService)
    {
        _embeddingService = embeddingService;
        _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "codesearchnet_subset.json");
    }

    public async Task InitializeDatasetAsync()
    {
        // 1. ÖNCELİK: Dataset Builder ile oluşturulan GERÇEK veritabanını ara
        var realDbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "codesearchnet_vector_db.json");
        
        // Eğer Debug klasöründe yoksa, kullanıcının kopyalamasına gerek kalmadan ana klasörde (Masaüstü/DeepCode YGA) arayalım:
        if (!File.Exists(realDbPath))
        {
            var projectRootDbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "codesearchnet_vector_db.json");
            if (File.Exists(projectRootDbPath))
            {
                realDbPath = projectRootDbPath;
            }
        }

        if (File.Exists(realDbPath))
        {
            var json = await File.ReadAllTextAsync(realDbPath);
            _dataset = JsonSerializer.Deserialize<List<CodeSearchItem>>(json) ?? new List<CodeSearchItem>();
            if (_dataset.Count > 0)
            {
                Log($"[RAG] ✓ {_dataset.Count} kayıt yüklendi: {realDbPath}");
                return;
            }
        }
        else
        {
            Log("[RAG] codesearchnet_vector_db.json bulunamadı, fallback veriler kullanılıyor.");
        }

        // 2. ÖNCELİK (Fallback): Eğer kullanıcı Dataset Builder'ı henüz çalıştırmadıysa, donmaları engellemek için kod içindeki altın kuralları kullan
        if (File.Exists(_databasePath))
        {
            var json = await File.ReadAllTextAsync(_databasePath);
            _dataset = JsonSerializer.Deserialize<List<CodeSearchItem>>(json) ?? new List<CodeSearchItem>();
            if (_dataset.Count > 10) return; 
        }

        var hardcodedData = new[]
        {
            new { Doc = "Always use using statements for IDisposable objects to prevent memory and connection leaks.", Code = "using (var conn = new SqlConnection(connectionString)) { conn.Open(); }" },
            new { Doc = "Use parameterized queries with SqlCommand to prevent SQL Injection.", Code = "var cmd = new SqlCommand(\"SELECT * FROM Users WHERE Username = @username\", conn); cmd.Parameters.AddWithValue(\"@username\", username);" },
            new { Doc = "Use StringBuilder when concatenating strings in a loop to avoid performance degradation.", Code = "var sb = new StringBuilder(); foreach (var item in list) { sb.Append(item); }" },
            new { Doc = "Catch specific exceptions instead of swallowing generic Exception, and log them.", Code = "catch (SqlException ex) { _logger.LogError(ex, \"Database error occurred.\"); throw; }" },
            new { Doc = "Use constructor injection (Dependency Injection) instead of tightly coupling components with new keyword.", Code = "public UserManager(IUserRepository repository) { _repository = repository; }" },
            new { Doc = "Use LINQ Any() method instead of Count > 0 for better performance when checking if a collection has elements.", Code = "if (users.Any()) { /* process */ }" },
            new { Doc = "Use StringComparison.OrdinalIgnoreCase for case-insensitive string comparisons instead of ToLower().", Code = "if (string.Equals(role, \"admin\", StringComparison.OrdinalIgnoreCase)) { /* do something */ }" },
            new { Doc = "Store magic numbers as named constants for better code readability and maintainability.", Code = "private const int MaxUserLimit = 50; if (userList.Count > MaxUserLimit) { /* ... */ }" },
            new { Doc = "Avoid returning null from methods returning collections; return an empty collection instead.", Code = "return Enumerable.Empty<User>();" },
            new { Doc = "Use async/await all the way down and avoid blocking async code with .Result or .Wait().", Code = "await ProcessDataAsync();" },
            new { Doc = "Use IReadOnlyList or IEnumerable for exposing collections to prevent unintended modifications.", Code = "public IReadOnlyList<string> Roles => _roles.AsReadOnly();" },
            new { Doc = "Always validate method arguments and throw ArgumentNullException for null inputs.", Code = "if (username == null) throw new ArgumentNullException(nameof(username));" }
        };

        _dataset = new List<CodeSearchItem>();
        int count = 0;
        foreach (var item in hardcodedData)
        {
            _dataset.Add(new CodeSearchItem
            {
                Id = $"CSN-FALLBACK-{count}",
                Description = item.Doc,
                CodeSnippet = item.Code
            });
            count++;
        }

        // Embedding (Vektör) işlemleri (Google API rate limitlerine takılmamak için 4'erli gruplar halinde asenkron yapıyoruz)
        var batchSize = 4;
        for (int i = 0; i < _dataset.Count; i += batchSize)
        {
            var batch = _dataset.Skip(i).Take(batchSize).ToList();
            var tasks = batch.Select(async item =>
            {
                string contentToEmbed = $"Document: {item.Description}\nCode: {item.CodeSnippet}";
                item.Vector = await _embeddingService.GetEmbeddingAsync(contentToEmbed);
            });
            
            await Task.WhenAll(tasks);
            await Task.Delay(1500); // Rate limit yasağı yememek için 1.5 sn bekleme
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        await File.WriteAllTextAsync(_databasePath, JsonSerializer.Serialize(_dataset, options));
    }

    public async Task<List<CodeSearchItem>> SearchSimilarAsync(float[] queryVector, int topK = 3)
    {
        if (!_dataset.Any()) await InitializeDatasetAsync();

        if (queryVector == null || queryVector.Length == 0)
        {
            Console.WriteLine("[RAG] ⚠ Sorgu vektörü boş, benzer kod araması yapılamıyor.");
            return new List<CodeSearchItem>();
        }

        var results = _dataset.Select(item => new
        {
            Item = item,
            Similarity = CosineSimilarity(queryVector, item.Vector)
        })
        .OrderByDescending(x => x.Similarity)
        .Take(topK)
        .ToList();

        if (results.Any())
            Log($"[RAG] ✓ En yakın eşleşme (benzerlik: {results[0].Similarity:F3}): {results[0].Item.Description?[..Math.Min(60, results[0].Item.Description?.Length ?? 0)]}...");
        else
            Log("[RAG] ⚠ Benzer kod bulunamadı (vektör boyutu uyuşmazlığı olabilir).");

        return results.Select(x => x.Item).ToList();
    }

    private static float CosineSimilarity(float[] v1, float[] v2)
    {
        if (v1 == null || v2 == null || v1.Length != v2.Length)
            return 0f;

        float dotProduct = 0f;
        float mag1 = 0f;
        float mag2 = 0f;

        for (int i = 0; i < v1.Length; i++)
        {
            dotProduct += v1[i] * v2[i];
            mag1 += v1[i] * v1[i];
            mag2 += v2[i] * v2[i];
        }

        mag1 = (float)Math.Sqrt(mag1);
        mag2 = (float)Math.Sqrt(mag2);

        if (mag1 == 0 || mag2 == 0) return 0f;

        return dotProduct / (mag1 * mag2);
    }

    private static void Log(string message)
    {
        try
        {
            string logPath = Path.Combine(Environment.CurrentDirectory, "rag_debug.log");
            File.AppendAllText(logPath, $"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }
        catch { /* Log hatası uygulamayı durdurmasın */ }
    }
}
