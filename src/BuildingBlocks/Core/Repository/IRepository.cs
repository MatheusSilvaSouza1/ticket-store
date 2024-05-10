using Core.Domain;

namespace Core.Repository;

public interface IRepository : IAsyncDisposable, IDisposable, IAggregateRoot
{
}