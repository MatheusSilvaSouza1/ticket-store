using Core.Domain;

namespace Core.Repository;

public interface IRepository<T> : IAsyncDisposable, IDisposable
    where T : class, IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}