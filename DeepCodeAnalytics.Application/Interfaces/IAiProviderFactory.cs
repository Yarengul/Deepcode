using DeepCodeAnalytics.Application.Enums;

namespace DeepCodeAnalytics.Application.Interfaces;

public interface IAiProviderFactory
{
    IGeminiService GetProvider(AiEngineType engineType);
}
