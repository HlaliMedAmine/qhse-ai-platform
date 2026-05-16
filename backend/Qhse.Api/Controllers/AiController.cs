using Microsoft.AspNetCore.Mvc;
using Qhse.Api.Contracts.Ai;
using Qhse.Api.Services;

namespace Qhse.Api.Controllers;

[ApiController]
[Route("api/ai")]
public sealed class AiController(IAiAnalysisService service) : ControllerBase
{
    [HttpPost("analyze")]
    [ProducesResponseType(typeof(AiAnalyzeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AiAnalyzeResponse>> Analyze(AiAnalyzeRequest request, CancellationToken cancellationToken)
    {
        var response = await service.AnalyzeAsync(request, cancellationToken);
        return Ok(response);
    }
}
