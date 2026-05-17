using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var filePath = @"C:\Users\cemde\OneDrive\Desktop\DeepCode YGA\csharp_codesearchnet.jsonl";
        
        var snippets = new List<(string doc, string code)>
        {
            ("Registers the IUserRepository into the dependency injection container.", "public static IServiceCollection AddRepositories(this IServiceCollection services) {\n    services.AddScoped<IUserRepository, UserRepository>();\n    return services;\n}"),
            ("Executes a SQL query securely using parameters to prevent SQL injection.", "public async Task<User> GetUserAsync(string username) {\n    using var conn = new SqlConnection(_connectionString);\n    var cmd = new SqlCommand(\"SELECT * FROM Users WHERE Username = @u\", conn);\n    cmd.Parameters.AddWithValue(\"@u\", username);\n    await conn.OpenAsync();\n    using var reader = await cmd.ExecuteReaderAsync();\n    if (await reader.ReadAsync()) return new User(reader[\"Id\"].ToString());\n    return null;\n}"),
            ("Concatenates strings efficiently using StringBuilder.", "public string BuildReport(List<string> lines) {\n    var sb = new StringBuilder();\n    foreach(var line in lines) {\n        sb.AppendLine(line);\n    }\n    return sb.ToString();\n}"),
            ("Handles exceptions properly by logging and rethrowing.", "public void ProcessFile(string path) {\n    try {\n        var content = File.ReadAllText(path);\n    } catch (IOException ex) {\n        _logger.LogError(ex, \"Error reading file\");\n        throw;\n    }\n}"),
            ("Checks if a collection is empty efficiently using LINQ Any.", "public bool HasActiveUsers(List<User> users) {\n    return users.Where(u => u.IsActive).Any();\n}"),
            ("Compares strings safely ignoring case.", "public bool IsAdmin(string role) {\n    return string.Equals(role, \"Admin\", StringComparison.OrdinalIgnoreCase);\n}"),
            ("Disposes unmanaged resources using the using statement.", "public void WriteLog() {\n    using (var fs = new FileStream(\"log.txt\", FileMode.Append)) {\n        using (var sw = new StreamWriter(fs)) {\n            sw.WriteLine(\"Log entry\");\n        }\n    }\n}"),
            ("Uses asynchronous programming to prevent thread blocking.", "public async Task ProcessDataAsync() {\n    var data = await FetchDataAsync();\n    await SaveDataAsync(data);\n}"),
            ("Validates arguments to prevent NullReferenceExceptions.", "public void UpdateProfile(UserProfile profile) {\n    if (profile == null) throw new ArgumentNullException(nameof(profile));\n    _db.Update(profile);\n}"),
            ("Uses ReadOnly collections to prevent unintended mutations.", "public IReadOnlyList<string> GetRoles() {\n    return _roles.AsReadOnly();\n}"),
            ("Avoids magic numbers by defining constants.", "private const int MaxRetryCount = 3;\npublic void RetryAction() {\n    for(int i=0; i<MaxRetryCount; i++) { TryAction(); }\n}"),
            ("Returns Enumerable.Empty instead of null to prevent null reference errors.", "public IEnumerable<string> GetTags(string item) {\n    if (string.IsNullOrEmpty(item)) return Enumerable.Empty<string>();\n    return item.Split(',');\n}"),
            ("Ensures task completion source is completed securely.", "public void CompleteTask(TaskCompletionSource<bool> tcs) {\n    tcs.TrySetResult(true);\n}"),
            ("Configures HttpClient properly to prevent socket exhaustion.", "services.AddHttpClient<IMyService, MyService>(client => {\n    client.BaseAddress = new Uri(\"https://api.example.com\");\n});"),
            ("Uses Object pooling to reduce garbage collection overhead.", "var buffer = ArrayPool<byte>.Shared.Rent(1024);\ntry {\n    stream.Read(buffer, 0, 1024);\n} finally {\n    ArrayPool<byte>.Shared.Return(buffer);\n}")
        };

        // We will generate 1000 items by repeating and slightly modifying them to simulate a large file
        using var writer = new StreamWriter(filePath);
        for (int i = 0; i < 1000; i++)
        {
            var snippet = snippets[i % snippets.Count];
            var doc = new
            {
                repo = "microsoft/corefx",
                path = "src/System.IO/File.cs",
                func_name = "Method_" + i,
                original_string = snippet.code.Replace("Action", "Action" + i),
                language = "csharp",
                code = snippet.code,
                code_tokens = new[] { "public", "void" },
                docstring = snippet.doc,
                docstring_tokens = new[] { "Doc" },
                sha = "abcdef123456",
                url = "https://github.com/microsoft"
            };
            
            writer.WriteLine(JsonSerializer.Serialize(doc));
        }

        Console.WriteLine("dataset.jsonl generated at " + filePath);
    }
}
