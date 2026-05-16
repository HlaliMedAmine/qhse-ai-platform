using Qhse.Api.Contracts.Incidents;

namespace Qhse.Api.Services;

public interface IIncidentService
{
    Task<IReadOnlyList<IncidentDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<IncidentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IncidentDto> CreateAsync(CreateIncidentRequest request, CancellationToken cancellationToken);
    Task<IncidentDto?> UpdateAsync(Guid id, UpdateIncidentRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
