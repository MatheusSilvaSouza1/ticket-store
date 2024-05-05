namespace Core.Repository;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}