using Qhse.Api.Contracts.Audits;

namespace Qhse.Api.Services;

public interface IAuditService
{
    Task<IReadOnlyList<AuditDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<AuditDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<AuditDto> CreateAsync(CreateAuditRequest request, CancellationToken cancellationToken);
    Task<AuditDto?> UpdateAsync(Guid id, UpdateAuditRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
