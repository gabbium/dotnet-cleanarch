namespace CleanArch;

public interface IRepository<TEntity> : IReadRepository<TEntity> where TEntity : class, IAggregateRoot
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
