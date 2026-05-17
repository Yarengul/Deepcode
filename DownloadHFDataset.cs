using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class DownloadHFDataset
{
    static async Task Main()
    {
        Console.WriteLine("HuggingFace API'sinden 'iamtarun/csharp-code-instructions' veri seti indiriliyor...");
        
        string datasetName = "iamtarun/csharp-code-instructions";
        string urlBase = $"https://datasets-server.huggingface.co/rows?dataset={Uri.EscapeDataString(datasetName)}&config=default&split=train";
        string outputFile = "hf_gercek_veri_seti.jsonl";

        using var httpClient = new HttpClient();
        using var writer = new StreamWriter(outputFile);

        // Şimdilik test ve gerçek kullanım için 2000 adet veri çekelim (API limiti 100'er 100'er izin veriyor)
        int totalRecords = 2000; 
        int batchSize = 100;

        for (int offset = 0; offset < totalRecords; offset += batchSize)
        {
            Console.WriteLine($"[+] API'den çekiliyor: {offset} - {offset + batchSize} arası kayıtlar...");
            string url = $"{urlBase}&offset={offset}&length={batchSize}";
            
            try
            {
                var response = await httpClient.GetStringAsync(url);
                using var jsonDoc = JsonDocument.Parse(response);
                var rows = jsonDoc.RootElement.GetProperty("rows");

                foreach (var row in rows.EnumerateArray())
                {
                    var rowData = row.GetProperty("row");
                    string instruction = rowData.GetProperty("instruction").GetString() ?? "";
                    string input = rowData.GetProperty("input").GetString() ?? "";
                    string output = rowData.GetProperty("output").GetString() ?? "";

                    // Bizim DatasetBuilder'ın (CodeSearchNet) beklediği JSONL formatına anında dönüştürüyoruz
                    var doc = new
                    {
                        repo = "huggingface/iamtarun",
                        path = "N/A",
                        func_name = "Instruction",
                        original_string = output, // "CodeSnippet" alanına denk gelecek asıl C# kodu
                        language = "csharp",
                        code = output,
                        code_tokens = Array.Empty<string>(),
                        docstring = $"{instruction}\n{input}".Trim(), // "Description" alanına denk gelecek soru/hedef
                        docstring_tokens = Array.Empty<string>(),
                        sha = "",
                        url = "https://huggingface.co/datasets/iamtarun/csharp-code-instructions"
                    };

                    await writer.WriteLineAsync(JsonSerializer.Serialize(doc));
                }
                
                // HuggingFace sunucularını yormamak ve Rate Limit yememek için ufak bir es verelim
                await Task.Delay(500); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"İndirme sırasında hata oluştu: {ex.Message}");
                break;
            }
        }

        Console.WriteLine($"\n[BAŞARILI] {totalRecords} adet gerçek C# eğitim verisi başarıyla indirildi!");
        Console.WriteLine($"Dosya: '{outputFile}' olarak kaydedildi.");
        Console.WriteLine("İleride DatasetBuilder uygulamasını çalıştırdığınızda dosya adı olarak bunu verebilirsiniz.");
    }
}
