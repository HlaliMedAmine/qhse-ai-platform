using Qhse.Api.Contracts.Ai;

namespace Qhse.Api.Services;

public interface IAiAnalysisService
{
    Task<AiAnalyzeResponse> AnalyzeAsync(AiAnalyzeRequest request, CancellationToken cancellationToken);
}
