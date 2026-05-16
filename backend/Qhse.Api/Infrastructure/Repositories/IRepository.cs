using Qhse.Api.Domain.Entities;

namespace Qhse.Api.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : AuditableEntity
{
    Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
