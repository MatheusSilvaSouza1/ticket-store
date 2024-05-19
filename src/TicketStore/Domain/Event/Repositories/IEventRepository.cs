using Core.Repository;

namespace Domain.Event.Repositories;

public interface IEventRepository : IBaseRepository<Events>
{
    public Task<Events?> FindEvent(Guid organizerId, Guid eventId);
}