using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class DownloadExpert
{
    static async Task Main()
    {
        Console.WriteLine("HuggingFace'ten MehdiFe/csharp-instruction-Dataset indiriliyor...");
        
        // Bu sefer açık ve erişilebilir bir API uç noktası kullanıyoruz
        string datasetName = "MehdiFe/csharp-instruction-Dataset";
        string urlBase = $"https://datasets-server.huggingface.co/rows?dataset={Uri.EscapeDataString(datasetName)}&config=default&split=train";
        string outputFile = "expert_csharp_dataset.jsonl";

        using var httpClient = new HttpClient();
        using var writer = new StreamWriter(outputFile);

        // Tam 1077 adet çekerek toplamı 1500'e tamamlıyoruz
        int totalRecords = 1077; 
        int batchSize = 100;

        for (int offset = 0; offset < totalRecords; offset += batchSize)
        {
            int currentBatch = Math.Min(batchSize, totalRecords - offset);
            Console.WriteLine($"[+] Uzman veriler çekiliyor: {offset} - {offset + currentBatch} arası...");
            
            string url = $"{urlBase}&offset={offset}&length={currentBatch}";
            
            try
            {
                var response = await httpClient.GetStringAsync(url);
                using var jsonDoc = JsonDocument.Parse(response);
                var rows = jsonDoc.RootElement.GetProperty("rows");

                foreach (var row in rows.EnumerateArray())
                {
                    var rowData = row.GetProperty("row");
                    
                    // Bu veri setindeki alanları (instruction, output) alıyoruz
                    string instruction = "";
                    if (rowData.TryGetProperty("instruction", out var inst)) instruction = inst.GetString() ?? "";
                    
                    string output = "";
                    if (rowData.TryGetProperty("output", out var outProp)) output = outProp.GetString() ?? "";

                    if (string.IsNullOrEmpty(instruction) || string.IsNullOrEmpty(output)) continue;

                    var doc = new
                    {
                        repo = "MehdiFe/csharp-instruction",
                        path = "expert_dataset",
                        func_name = "ExpertLogic",
                        original_string = output,
                        language = "csharp",
                        code = output,
                        code_tokens = Array.Empty<string>(),
                        docstring = instruction,
                        docstring_tokens = Array.Empty<string>(),
                        sha = "",
                        url = "https://huggingface.co/datasets/MehdiFe/csharp-instruction-Dataset"
                    };

                    await writer.WriteLineAsync(JsonSerializer.Serialize(doc));
                }
                
                await Task.Delay(200); // API'yi yormayalım
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                break;
            }
        }

        Console.WriteLine($"\n[BAŞARILI] {totalRecords} adet uzman C# verisi '{outputFile}' dosyasına eklendi.");
    }
}
