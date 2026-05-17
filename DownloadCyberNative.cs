using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

class DownloadCyberNative
{
    static async Task Main()
    {
        Console.WriteLine("HuggingFace'ten CyberNative Güvenlik Veri Seti indiriliyor...");
        string url = "https://huggingface.co/datasets/CyberNative/Code_Vulnerability_Security_DPO/resolve/main/secure_programming_dpo.json";
        string outputFile = "cybernative_dataset.jsonl";

        using var httpClient = new HttpClient();
        try
        {
            Console.WriteLine("Dosya indiriliyor...");
            var jsonString = await httpClient.GetStringAsync(url);
            
            if (string.IsNullOrWhiteSpace(jsonString)) {
                Console.WriteLine("Hata: Dosya boş!");
                return;
            }

            var langStats = new Dictionary<string, int>();
            using var writer = new StreamWriter(outputFile);
            int count = 0;

            using var reader = new StringReader(jsonString);
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                line = line.Trim();
                if (string.IsNullOrWhiteSpace(line) || line == "[" || line == "]") continue;
                if (line.EndsWith(",")) line = line.Substring(0, line.Length - 1);

                try 
                {
                    using var item = JsonDocument.Parse(line);
                    var root = item.RootElement;
                    
                    string lang = "bilinmiyor";
                    if (root.TryGetProperty("lang", out var l)) lang = l.GetString()?.ToLower() ?? "bilinmiyor";
                    
                    if (!langStats.ContainsKey(lang)) langStats[lang] = 0;
                    langStats[lang]++;

                    if (lang == "csharp" || lang == "c#" || lang == "cs" || lang.Contains("c#"))
                    {
                        if (ProcessItem(root, writer, ref count))
                        {
                            if (count >= 5000) break;
                        }
                    }
                }
                catch { }
            }

            Console.WriteLine("\n--- VERİ SETİ DİL DAĞILIMI RAPORU ---");
            foreach (var stat in langStats.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"- {stat.Key.ToUpper()}: {stat.Value} adet");
            }
            Console.WriteLine("-------------------------------------\n");

            Console.WriteLine($"[SONUÇ] Toplam {count} adet C# verisi '{outputFile}' dosyasına kaydedildi.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
        }
    }

    private static bool ProcessItem(JsonElement root, StreamWriter writer, ref int count)
    {
        try
        {
            string prompt = "";
            if (root.TryGetProperty("prompt", out var p1)) prompt = p1.GetString();
            else if (root.TryGetProperty("instruction", out var p2)) prompt = p2.GetString();
            else if (root.TryGetProperty("question", out var p3)) prompt = p3.GetString();

            string code = "";
            if (root.TryGetProperty("chosen", out var c1)) code = c1.GetString();
            else if (root.TryGetProperty("output", out var c2)) code = c2.GetString();
            else if (root.TryGetProperty("answer", out var c3)) code = c3.GetString();

            if (string.IsNullOrEmpty(prompt) || string.IsNullOrEmpty(code)) return false;

            var doc = new
            {
                repo = "CyberNative/Security_DPO",
                path = "security_vulnerability",
                func_name = "VulnerabilityFix",
                original_string = code,
                language = "csharp",
                code = code,
                code_tokens = Array.Empty<string>(),
                docstring = prompt,
                docstring_tokens = Array.Empty<string>(),
                sha = "",
                url = "https://huggingface.co/datasets/CyberNative/Code_Vulnerability_Security_DPO"
            };

            writer.WriteLine(JsonSerializer.Serialize(doc));
            count++;
            return true;
        }
        catch { return false; }
    }
}
