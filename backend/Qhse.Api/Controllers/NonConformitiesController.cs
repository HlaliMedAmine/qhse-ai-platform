using Microsoft.AspNetCore.Mvc;
using Qhse.Api.Contracts.NonConformities;
using Qhse.Api.Services;

namespace Qhse.Api.Controllers;

[ApiController]
[Route("api/nonconformities")]
public sealed class NonConformitiesController(INonConformityService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<NonConformityDto>>> GetAll(CancellationToken cancellationToken) => Ok(await service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NonConformityDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await service.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<NonConformityDto>> Create(CreateNonConformityRequest request, CancellationToken cancellationToken)
    {
        var item = await service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<NonConformityDto>> Update(Guid id, UpdateNonConformityRequest request, CancellationToken cancellationToken)
    {
        var item = await service.UpdateAsync(id, request, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) => await service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}
