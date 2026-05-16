using Qhse.Api.Contracts.NonConformities;

namespace Qhse.Api.Services;

public interface INonConformityService
{
    Task<IReadOnlyList<NonConformityDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<NonConformityDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<NonConformityDto> CreateAsync(CreateNonConformityRequest request, CancellationToken cancellationToken);
    Task<NonConformityDto?> UpdateAsync(Guid id, UpdateNonConformityRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
