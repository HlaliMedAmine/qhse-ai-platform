using Qhse.Api.Contracts.NonConformities;
using Qhse.Api.Domain.Entities;
using Qhse.Api.Domain.Enums;
using Qhse.Api.Infrastructure.Repositories;

namespace Qhse.Api.Services;

public sealed class NonConformityService(IRepository<NonConformity> repository) : INonConformityService
{
    public async Task<IReadOnlyList<NonConformityDto>> GetAllAsync(CancellationToken cancellationToken) => (await repository.ListAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<NonConformityDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<NonConformityDto> CreateAsync(CreateNonConformityRequest request, CancellationToken cancellationToken)
    {
        if (!EnumParser.TryParse<Severity>(request.Severity, out var severity))
        {
            throw new ArgumentException("Invalid severity.", nameof(request));
        }

        var entity = new NonConformity
        {
            Reference = ReferenceGenerator.New("NC"),
            Title = request.Title,
            SourceReference = request.Source,
            Severity = severity,
            Owner = request.Owner,
            DueDate = request.DueDate,
            Status = RecordStatus.Open,
            IncidentId = request.IncidentId,
            AuditId = request.AuditId
        };

        await repository.AddAsync(entity, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return ToDto(entity);
    }

    public async Task<NonConformityDto?> UpdateAsync(Guid id, UpdateNonConformityRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return null;
        if (!EnumParser.TryParse<Severity>(request.Severity, out var severity) ||
            !EnumParser.TryParse<RecordStatus>(request.Status, out var status))
        {
            throw new ArgumentException("Invalid severity or status.", nameof(request));
        }

        entity.Title = request.Title;
        entity.SourceReference = request.Source;
        entity.Severity = severity;
        entity.Owner = request.Owner;
        entity.DueDate = request.DueDate;
        entity.Status = status;
        entity.IncidentId = request.IncidentId;
        entity.AuditId = request.AuditId;
        repository.Update(entity);
        await repository.SaveChangesAsync(cancellationToken);
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        repository.Delete(entity);
        await repository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static NonConformityDto ToDto(NonConformity entity) => new(
        entity.Id,
        entity.Reference,
        entity.Title,
        entity.SourceReference,
        entity.Severity.ToApiValue(),
        entity.Owner,
        entity.DueDate,
        entity.Status.ToApiValue(),
        entity.IncidentId,
        entity.AuditId);
}
