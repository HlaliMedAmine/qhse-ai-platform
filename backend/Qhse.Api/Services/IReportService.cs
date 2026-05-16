using Qhse.Api.Contracts.Reports;

namespace Qhse.Api.Services;

public interface IReportService
{
    Task<IReadOnlyList<ReportDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ReportDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ReportDto> CreateAsync(CreateReportRequest request, CancellationToken cancellationToken);
    Task<ReportDto?> UpdateAsync(Guid id, UpdateReportRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
