using Qhse.Api.Contracts.Risks;

namespace Qhse.Api.Services;

public interface IRiskService
{
    Task<IReadOnlyList<RiskDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<RiskDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<RiskDto> CreateAsync(CreateRiskRequest request, CancellationToken cancellationToken);
    Task<RiskDto?> UpdateAsync(Guid id, UpdateRiskRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
