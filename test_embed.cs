using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "AIzaSyBMyUUKfVeMxYfCMpeQzWGTmKbMGGSAtDY"; // Kullanıcının paylaştığı key
        string url = $"https://generativelanguage.googleapis.com/v1beta/models/text-embedding-004:embedContent?key={apiKey}";
        
        string json = @"{
            ""model"": ""models/text-embedding-004"",
            ""content"": {
                ""parts"": [{ ""text"": ""hello"" }]
            }
        }";

        using var client = new HttpClient();
        var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        
        Console.WriteLine($"Status: {response.StatusCode}");
        Console.WriteLine($"Body: {await response.Content.ReadAsStringAsync()}");
    }
}
