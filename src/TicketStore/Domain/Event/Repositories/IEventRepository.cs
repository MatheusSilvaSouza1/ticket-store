using Core.Repository;
using Domain.Event.DTOs;

namespace Domain.Event.Repositories;

public interface IEventRepository : IBaseRepository<Events>
{
    Task<Events?> FindEvent(Guid organizerId, Guid eventId, CancellationToken cancellationToken);
    Task<List<EventsResponseDTO>> GetEventsDTO(CancellationToken cancellationToken);
}