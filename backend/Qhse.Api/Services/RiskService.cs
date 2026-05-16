using Qhse.Api.Contracts.Risks;
using Qhse.Api.Domain.Entities;
using Qhse.Api.Domain.Enums;
using Qhse.Api.Infrastructure.Repositories;

namespace Qhse.Api.Services;

public sealed class RiskService(IRepository<Risk> repository) : IRiskService
{
    public async Task<IReadOnlyList<RiskDto>> GetAllAsync(CancellationToken cancellationToken) => (await repository.ListAsync(cancellationToken)).Select(ToDto).ToList();

    public async Task<RiskDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<RiskDto> CreateAsync(CreateRiskRequest request, CancellationToken cancellationToken)
    {
        if (!EnumParser.TryParse<RiskCategory>(request.Category, out var category))
        {
            throw new ArgumentException("Invalid risk category.", nameof(request));
        }

        var entity = new Risk
        {
            Reference = ReferenceGenerator.New("R"),
            Hazard = request.Hazard,
            Category = category,
            Likelihood = request.Likelihood,
            Impact = request.Impact,
            Mitigation = request.Mitigation
        };

        await repository.AddAsync(entity, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return ToDto(entity);
    }

    public async Task<RiskDto?> UpdateAsync(Guid id, UpdateRiskRequest request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return null;
        if (!EnumParser.TryParse<RiskCategory>(request.Category, out var category))
        {
            throw new ArgumentException("Invalid risk category.", nameof(request));
        }

        entity.Hazard = request.Hazard;
        entity.Category = category;
        entity.Likelihood = request.Likelihood;
        entity.Impact = request.Impact;
        entity.Mitigation = request.Mitigation;
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

    private static RiskDto ToDto(Risk entity)
    {
        var score = entity.Likelihood * entity.Impact;
        return new RiskDto(entity.Id, entity.Reference, entity.Hazard, entity.Category.ToApiValue(), entity.Likelihood, entity.Impact, score, RiskScoring.GetLevel(score).ToApiValue(), entity.Mitigation);
    }
}
