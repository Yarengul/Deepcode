using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class DownloadArtifactAI
{
    static async Task Main()
    {
        Console.WriteLine("HuggingFace'ten Artifact-AI/csharp-instruction-dataset indiriliyor...");
        
        string datasetName = "Artifact-AI/csharp-instruction-dataset";
        string urlBase = $"https://datasets-server.huggingface.co/rows?dataset={Uri.EscapeDataString(datasetName)}&config=default&split=train";
        string outputFile = "artifact_ai_csharp.jsonl";

        using var httpClient = new HttpClient();
        using var writer = new StreamWriter(outputFile);

        // Kullanıcının istediği gibi tam 1077 adet çekiyoruz
        int totalRecords = 1077; 
        int batchSize = 100;

        for (int offset = 0; offset < totalRecords; offset += batchSize)
        {
            int currentBatch = Math.Min(batchSize, totalRecords - offset);
            Console.WriteLine($"[+] İndiriliyor: {offset} - {offset + currentBatch} arası kayıtlar...");
            
            string url = $"{urlBase}&offset={offset}&length={currentBatch}";
            
            try
            {
                var response = await httpClient.GetStringAsync(url);
                using var jsonDoc = JsonDocument.Parse(response);
                var rows = jsonDoc.RootElement.GetProperty("rows");

                foreach (var row in rows.EnumerateArray())
                {
                    var rowData = row.GetProperty("row");
                    
                    // Artifact-AI formatındaki 'instruction' ve 'output' alanlarını alıyoruz
                    string instruction = "";
                    if (rowData.TryGetProperty("instruction", out var inst)) instruction = inst.GetString() ?? "";
                    
                    string output = "";
                    if (rowData.TryGetProperty("output", out var outProp)) output = outProp.GetString() ?? "";

                    if (string.IsNullOrEmpty(instruction) || string.IsNullOrEmpty(output)) continue;

                    var doc = new
                    {
                        repo = "Artifact-AI/csharp-instruction",
                        path = "instruction_dataset",
                        func_name = "InstructionResponse",
                        original_string = output, // Kod cevabı
                        language = "csharp",
                        code = output,
                        code_tokens = Array.Empty<string>(),
                        docstring = instruction, // Soru/Talimat
                        docstring_tokens = Array.Empty<string>(),
                        sha = "",
                        url = "https://huggingface.co/datasets/Artifact-AI/csharp-instruction-dataset"
                    };

                    await writer.WriteLineAsync(JsonSerializer.Serialize(doc));
                }
                
                await Task.Delay(300); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                break;
            }
        }

        Console.WriteLine($"\n[BAŞARILI] {totalRecords} adet gerçek C# mimari verisi '{outputFile}' dosyasına kaydedildi!");
    }
}
