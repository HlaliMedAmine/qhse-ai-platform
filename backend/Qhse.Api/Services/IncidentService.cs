using Qhse.Api.Contracts.Incidents;
using Qhse.Api.Domain.Entities;
using Qhse.Api.Domain.Enums;
using Qhse.Api.Infrastructure.Repositories;

namespace Qhse.Api.Services;

public sealed class IncidentService(IRepository<Incident> repository) : IIncidentService
{
    public async Task<IReadOnlyList<IncidentDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var items = await repository.ListAsync(cancellationToken);
        return items.Select(ToDto).ToList();
    }

    public async Task<IncidentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<IncidentDto> CreateAsync(CreateIncidentRequest request, CancellationToken cancellationToken)
    {
        if (!EnumParser.TryParse<Severity>(request.Severity, out var severity))
        {
            throw new ArgumentException("Invalid severity.", nameof(request));
        }

        var entity = new Incident
        {
            Reference = ReferenceGenerator.New("INC"),
            Title = request.Title,
            Site = request.Site,
            Severity = severity,
            Status = RecordStatus.Open,
            ReportedBy = request.ReportedBy,
            IncidentDate = request.Date,
            Description = request.Description
        };

        await repository.AddAsync(entity, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return ToDto(entity);
    }

    public async Task<IncidentDto?> UpdateAsync(Guid id, UpdateIncidentRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (!EnumParser.TryParse<Severity>(request.Severity, out var severity) ||
            !EnumParser.TryParse<RecordStatus>(request.Status, out var status))
        {
            throw new ArgumentException("Invalid severity or status.", nameof(request));
        }

        entity.Title = request.Title;
        entity.Site = request.Site;
        entity.Severity = severity;
        entity.Status = status;
        entity.ReportedBy = request.ReportedBy;
        entity.IncidentDate = request.Date;
        entity.Description = request.Description;

        repository.Update(entity);
        await repository.SaveChangesAsync(cancellationToken);
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        repository.Delete(entity);
        await repository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static IncidentDto ToDto(Incident entity)
    {
        return new IncidentDto(
            entity.Id,
            entity.Reference,
            entity.Title,
            entity.Site,
            entity.Severity.ToApiValue(),
            entity.Status.ToApiValue(),
            entity.ReportedBy,
            entity.IncidentDate,
            entity.Description);
    }
}
