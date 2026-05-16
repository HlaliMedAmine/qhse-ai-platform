using Microsoft.AspNetCore.Mvc;
using Qhse.Api.Contracts.Incidents;
using Qhse.Api.Services;

namespace Qhse.Api.Controllers;

[ApiController]
[Route("api/incidents")]
public sealed class IncidentsController(IIncidentService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<IncidentDto>>> GetAll(CancellationToken cancellationToken) => Ok(await service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IncidentDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await service.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<IncidentDto>> Create(CreateIncidentRequest request, CancellationToken cancellationToken)
    {
        var item = await service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IncidentDto>> Update(Guid id, UpdateIncidentRequest request, CancellationToken cancellationToken)
    {
        var item = await service.UpdateAsync(id, request, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
    }
}
