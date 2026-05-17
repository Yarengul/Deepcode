namespace DeepCodeAnalytics.Domain.Models;

public class CodeSearchItem
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CodeSnippet { get; set; } = string.Empty;
    public float[] Vector { get; set; } = System.Array.Empty<float>();
}
