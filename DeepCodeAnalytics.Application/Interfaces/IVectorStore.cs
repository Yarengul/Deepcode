using System.Collections.Generic;
using System.Threading.Tasks;
using DeepCodeAnalytics.Domain.Models;

namespace DeepCodeAnalytics.Application.Interfaces;

public interface IVectorStore
{
    Task InitializeDatasetAsync();
    Task<List<CodeSearchItem>> SearchSimilarAsync(float[] queryVector, int topK = 3);
}
