using Microsoft.AspNetCore.Mvc;
using Qhse.Api.Contracts.Risks;
using Qhse.Api.Services;

namespace Qhse.Api.Controllers;

[ApiController]
[Route("api/risks")]
public sealed class RisksController(IRiskService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RiskDto>>> GetAll(CancellationToken cancellationToken) => Ok(await service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RiskDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await service.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<RiskDto>> Create(CreateRiskRequest request, CancellationToken cancellationToken)
    {
        var item = await service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RiskDto>> Update(Guid id, UpdateRiskRequest request, CancellationToken cancellationToken)
    {
        var item = await service.UpdateAsync(id, request, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) => await service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}
