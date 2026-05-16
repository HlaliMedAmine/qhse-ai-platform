using Microsoft.AspNetCore.Mvc;
using Qhse.Api.Contracts.Reports;
using Qhse.Api.Services;

namespace Qhse.Api.Controllers;

[ApiController]
[Route("api/reports")]
public sealed class ReportsController(IReportService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ReportDto>>> GetAll(CancellationToken cancellationToken) => Ok(await service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReportDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await service.GetByIdAsync(id, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ReportDto>> Create(CreateReportRequest request, CancellationToken cancellationToken)
    {
        var item = await service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ReportDto>> Update(Guid id, UpdateReportRequest request, CancellationToken cancellationToken)
    {
        var item = await service.UpdateAsync(id, request, cancellationToken);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) => await service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}
