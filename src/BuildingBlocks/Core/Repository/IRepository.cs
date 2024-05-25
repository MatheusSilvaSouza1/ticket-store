using Core.Domain;

namespace Core.Repository;

public interface IRepository<T>
    where T : AggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}