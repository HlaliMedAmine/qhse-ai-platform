using Qhse.Api.Contracts.Audits;
using Qhse.Api.Domain.Entities;
using Qhse.Api.Domain.Enums;
using Qhse.Api.Infrastructure.Repositories;

namespace Qhse.Api.Services;

public sealed class AuditService(IRepository<Audit> repository) : IAuditService
{
    public async Task<IReadOnlyList<AuditDto>> GetAllAsync(CancellationToken cancellationToken) => (await repository.ListAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<AuditDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<AuditDto> CreateAsync(CreateAuditRequest request, CancellationToken cancellationToken)
    {
        if (!EnumParser.TryParse<AuditStatus>(request.Status, out var status))
        {
            throw new ArgumentException("Invalid audit status.", nameof(request));
        }

        var entity = new Audit
        {
            Reference = request.Reference,
            Scope = request.Scope,
            Auditor = request.Auditor,
            AuditDate = request.Date,
            Score = request.Score,
            Status = status
        };

        await repository.AddAsync(entity, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return ToDto(entity);
    }

    public async Task<AuditDto?> UpdateAsync(Guid id, UpdateAuditRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return null;
        if (!EnumParser.TryParse<AuditStatus>(request.Status, out var status))
        {
            throw new ArgumentException("Invalid audit status.", nameof(request));
        }

        entity.Reference = request.Reference;
        entity.Scope = request.Scope;
        entity.Auditor = request.Auditor;
        entity.AuditDate = request.Date;
        entity.Score = request.Score;
        entity.Status = status;
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

    private static AuditDto ToDto(Audit entity) => new(entity.Id, entity.Reference, entity.Scope, entity.Auditor, entity.AuditDate, entity.Score, entity.Status.ToApiValue());
}
