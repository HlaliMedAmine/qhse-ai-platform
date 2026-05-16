using Microsoft.EntityFrameworkCore;
using Qhse.Api.Data;
using Qhse.Api.Domain.Entities;

namespace Qhse.Api.Infrastructure.Repositories;

public sealed class EfRepository<TEntity>(QhseDbContext dbContext) : IRepository<TEntity>
    where TEntity : AuditableEntity
{
    public async Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Set<TEntity>()
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<TEntity>().FindAsync([id], cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        entity.UpdatedAtUtc = DateTimeOffset.UtcNow;
        dbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
