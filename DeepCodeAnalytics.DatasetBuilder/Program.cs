using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DeepCodeAnalytics.Domain.Models;

namespace DeepCodeAnalytics.DatasetBuilder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("    DeepCode RAG Veri Seti Oluşturucu (Builder)");
            Console.WriteLine("==================================================");
            Console.WriteLine("[⚡] LOKAL TF-IDF MODU — API gerekmez, saniyeler içinde tamamlanır!\n");

            Console.WriteLine("1. Lütfen CodeSearchNet C# veri setinden (örn. valid.jsonl) indirdiğiniz dosyanın tam yolunu girin:");
            Console.WriteLine("   (Dosyanız yoksa HuggingFace veya S3'ten .jsonl uzantılı bir dosya indirin)");
            string filePath = "";
            while (true)
            {
                Console.Write("> ");
                filePath = Console.ReadLine()?.Trim('"', '\'', ' ') ?? "";
                if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
                    break;
                Console.WriteLine("HATA: Dosya bulunamadı. Lütfen geçerli bir yol girin:");
            }

            Console.Write("2. Kaç adet C# fonksiyonunu vektörleştirmek istiyorsunuz? (Önerilen: 1000-2000) > ");
            if (!int.TryParse(Console.ReadLine(), out int maxRecords))
                maxRecords = 1000;

            Console.WriteLine($"\n[+] {filePath} okunuyor...");

            var dataset = new List<CodeSearchItem>();
            int count = 0;

            using (var reader = new StreamReader(filePath))
            {
                while (count < maxRecords && !reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    try
                    {
                        using var doc = JsonDocument.Parse(line);
                        var root = doc.RootElement;
                        var docString = root.GetProperty("docstring").GetString();
                        var originalString = root.GetProperty("original_string").GetString();
                        if (!string.IsNullOrWhiteSpace(docString) && !string.IsNullOrWhiteSpace(originalString))
                        {
                            dataset.Add(new CodeSearchItem
                            {
                                Id = $"CSN-{count}",
                                Description = docString,
                                CodeSnippet = originalString
                            });
                            count++;
                        }
                    }
                    catch { /* Parse hatası, satırı atla */ }
                }
            }

            Console.WriteLine($"[+] {dataset.Count} geçerli kayıt okundu.");

            // CyberNative güvenlik veri setini de ekle (varsa)
            string cyberPath = "cybernative_dataset.jsonl";
            if (File.Exists(cyberPath))
            {
                Console.WriteLine($"[+] CyberNative güvenlik veri seti bulundu, ekleniyor...");
                int cyberCount = 0;
                using var reader = new StreamReader(cyberPath);
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    try
                    {
                        using var doc = JsonDocument.Parse(line);
                        var root = doc.RootElement;
                        var docString = root.GetProperty("docstring").GetString();
                        var originalString = root.GetProperty("original_string").GetString();
                        if (!string.IsNullOrWhiteSpace(docString) && !string.IsNullOrWhiteSpace(originalString))
                        {
                            dataset.Add(new CodeSearchItem
                            {
                                Id = $"CYBER-{cyberCount}",
                                Description = docString,
                                CodeSnippet = originalString
                            });
                            cyberCount++;
                        }
                    }
                    catch { }
                }
                Console.WriteLine($"[+] {cyberCount} CyberNative kaydı eklendi.");
            }

            Console.WriteLine($"[*] Toplam {dataset.Count} kayıt için TF-IDF vektörleri hesaplanıyor...");

            // TF-IDF hesaplama (tamamen lokal, API yok!)
            var texts = dataset.Select(d => $"{d.Description} {d.CodeSnippet}").ToList();
            var vectors = ComputeTfIdf(texts, vocabSize: 2048);

            for (int i = 0; i < dataset.Count; i++)
            {
                dataset[i].Vector = vectors[i];
                if ((i + 1) % 50 == 0 || i == dataset.Count - 1)
                    Console.Write($"\r[>] Vektörleme: {i + 1}/{dataset.Count} tamamlandı.   ");
            }

            Console.WriteLine();
            var outputPath = "codesearchnet_vector_db.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            await File.WriteAllTextAsync(outputPath, JsonSerializer.Serialize(dataset, options));

            // Vocab ve IDF ağırlıklarını da kaydet (sorgu vektörleştirme için gerekli)
            var vocabData = new { Vocab = _lastVocab, Idf = _lastIdf };
            await File.WriteAllTextAsync("tfidf_vocab.json", JsonSerializer.Serialize(vocabData, options));

            Console.WriteLine($"\n[BAŞARILI] {dataset.Count} kayıt '{outputPath}' dosyasına kaydedildi!");
            Console.WriteLine($"[BAŞARILI] Kelime dağarcığı 'tfidf_vocab.json' dosyasına kaydedildi!");
            Console.WriteLine($"[→] Şimdi şu komutu çalıştırın:");
            Console.WriteLine($"    Copy-Item codesearchnet_vector_db.json,tfidf_vocab.json DeepCodeAnalytics.UI\\ -Force");
            Console.ReadLine();
        }

        static string[] _lastVocab = Array.Empty<string>();
        static double[] _lastIdf = Array.Empty<double>();

        /// <summary>
        /// Metni kelimelere ayırır (kod ve doğal dil uyumlu tokenizer).
        /// </summary>
        static List<string> Tokenize(string text)
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

        /// <summary>
        /// TF-IDF vektörleri hesaplar. Sonuçlar L2-normalize edilmiş float dizileridir.
        /// LocalVectorStore'daki cosine similarity ile doğrudan uyumludur.
        /// </summary>
        static List<float[]> ComputeTfIdf(List<string> texts, int vocabSize = 2048)
        {
            Console.WriteLine("[*] Kelime dağarcığı oluşturuluyor...");

            // Adım 1: Tüm belgeleri tokenize et
            var tokenizedDocs = texts.Select(Tokenize).ToList();

            // Adım 2: Belge frekansı (DF) hesapla
            var docFreq = new Dictionary<string, int>();
            foreach (var tokens in tokenizedDocs)
            {
                foreach (var term in tokens.Distinct())
                {
                    if (!docFreq.ContainsKey(term)) docFreq[term] = 0;
                    docFreq[term]++;
                }
            }

            // Adım 3: En yüksek DF'ye sahip vocabSize terim seç
            var vocab = docFreq
                .OrderByDescending(kv => kv.Value)
                .Take(vocabSize)
                .Select(kv => kv.Key)
                .ToList();

            var vocabIndex = vocab.Select((term, idx) => (term, idx))
                                  .ToDictionary(x => x.term, x => x.idx);

            Console.WriteLine($"[*] {vocab.Count} terimlik kelime dağarcığı hazır.");

            int N = texts.Count;
            // IDF: smoothed = log((N+1)/(df+1)) + 1
            var idf = vocab.Select(term =>
                Math.Log((N + 1.0) / (docFreq[term] + 1.0)) + 1.0
            ).ToArray();

            _lastVocab = vocab.ToArray();
            _lastIdf = idf;

            // Adım 4: Her belge için TF-IDF vektörü hesapla
            var result = new List<float[]>(texts.Count);
            foreach (var tokens in tokenizedDocs)
            {
                var tf = new Dictionary<string, double>();
                foreach (var token in tokens)
                {
                    if (!tf.ContainsKey(token)) tf[token] = 0;
                    tf[token]++;
                }

                double docLen = tokens.Count > 0 ? tokens.Count : 1;
                foreach (var key in tf.Keys.ToList())
                    tf[key] /= docLen;

                // ÖNEMLI: vocab.Count kullan, vocabSize değil! (gerçek boyut ile TfIdfEmbeddingService uyumlu olsun)
                var vector = new float[vocab.Count];
                foreach (var (term, termTf) in tf)
                {
                    if (vocabIndex.TryGetValue(term, out int idx))
                        vector[idx] = (float)(termTf * idf[idx]);
                }

                // L2 normalize (cosine similarity için)
                float norm = (float)Math.Sqrt(vector.Sum(v => (double)v * v));
                if (norm > 0)
                    for (int i = 0; i < vector.Length; i++)
                        vector[i] /= norm;

                result.Add(vector);
            }

            return result;
        }
    }
}
