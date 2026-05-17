using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DeepCodeAnalytics.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DeepCodeAnalytics.Infrastructure.Services;

/// <summary>
/// TF-IDF tabanlı lokal embedding servisi.
/// Gemini API gerektirmez; tfidf_vocab.json dosyasından kelime dağarcığını yükler.
/// DatasetBuilder tarafından üretilen vektörlerle tam uyumludur.
/// </summary>
public class TfIdfEmbeddingService : IEmbeddingService
{
    private readonly string[] _vocab;
    private readonly double[] _idf;
    private readonly Dictionary<string, int> _vocabIndex;
    private readonly int _vocabSize;

    public TfIdfEmbeddingService(IConfiguration configuration)
    {
        // tfidf_vocab.json dosyasını çalışma dizininde veya UI altında ara
        string vocabPath = FindVocabFile();

        if (!File.Exists(vocabPath))
        {
            // Vocab dosyası yoksa boş vektörler döner (RAG devre dışı kalır ama sistem çalışır)
            _vocab = Array.Empty<string>();
            _idf = Array.Empty<double>();
            _vocabIndex = new Dictionary<string, int>();
            _vocabSize = 0;
            Console.WriteLine("[UYARI] tfidf_vocab.json bulunamadı. RAG devre dışı.");
            return;
        }

        var json = File.ReadAllText(vocabPath);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        _vocab = root.GetProperty("Vocab").EnumerateArray()
                     .Select(v => v.GetString() ?? "")
                     .ToArray();

        _idf = root.GetProperty("Idf").EnumerateArray()
                   .Select(v => v.GetDouble())
                   .ToArray();

        _vocabSize = _vocab.Length;
        _vocabIndex = _vocab.Select((term, idx) => (term, idx))
                            .ToDictionary(x => x.term, x => x.idx);

        Console.WriteLine($"[TF-IDF] {_vocabSize} terimlik kelime dağarcığı yüklendi.");
    }

    public Task<float[]> GetEmbeddingAsync(string text, CancellationToken cancellationToken = default)
    {
        if (_vocabSize == 0 || string.IsNullOrWhiteSpace(text))
            return Task.FromResult(Array.Empty<float>());

        var tokens = Tokenize(text);
        var tf = new Dictionary<string, double>();

        foreach (var token in tokens)
        {
            if (!tf.ContainsKey(token)) tf[token] = 0;
            tf[token]++;
        }

        double docLen = tokens.Count > 0 ? tokens.Count : 1;
        foreach (var key in tf.Keys.ToList())
            tf[key] /= docLen;

        var vector = new float[_vocabSize];
        foreach (var (term, termTf) in tf)
        {
            if (_vocabIndex.TryGetValue(term, out int idx))
                vector[idx] = (float)(termTf * _idf[idx]);
        }

        // L2 normalize (cosine similarity ile uyumlu)
        float norm = (float)Math.Sqrt(vector.Sum(v => (double)v * v));
        if (norm > 0)
            for (int i = 0; i < vector.Length; i++)
                vector[i] /= norm;

        return Task.FromResult(vector);
    }

    private static List<string> Tokenize(string text)
    {
        return text.ToLowerInvariant()
                   .Split(new char[]
                   {
                       ' ', '\t', '\n', '\r', '.', ',', ';', ':', '(', ')', '{', '}',
                       '[', ']', '<', '>', '/', '\\', '"', '\'', '-', '_', '=', '+',
                       '*', '!', '?', '@', '#', '$', '%', '^', '&', '|', '~', '`'
                   }, StringSplitOptions.RemoveEmptyEntries)
                   .Where(t => t.Length > 2 && t.Length < 30)
                   .ToList();
    }

    private static string FindVocabFile()
    {
        // Önce mevcut dizine bak
        string local = Path.Combine(Environment.CurrentDirectory, "tfidf_vocab.json");
        if (File.Exists(local)) return local;

        // Sonra UI klasörüne bak
        string ui = Path.Combine(Environment.CurrentDirectory, "DeepCodeAnalytics.UI", "tfidf_vocab.json");
        if (File.Exists(ui)) return ui;

        // bin/Debug altına bak
        string bin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tfidf_vocab.json");
        return bin;
    }
}
