using System.Threading;
using System.Threading.Tasks;

namespace DeepCodeAnalytics.Application.Interfaces;

public interface IEmbeddingService
{
    /// <summary>
    /// Verilen metni veya kodu Gemini API kullanarak bir sayısal vektöre (float array) dönüştürür.
    /// </summary>
    Task<float[]> GetEmbeddingAsync(string text, CancellationToken cancellationToken = default);
}
