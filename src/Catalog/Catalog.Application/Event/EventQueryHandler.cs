using Application.Event.Queries;
using Core.Cache;
using Domain.Event.DTOs;
using Domain.Event.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Event;

public class EventQueryHandler :
    IRequestHandler<GetEventsQuery, List<EventsResponseDTO>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IDistributedCache _cache;

    public EventQueryHandler(IEventRepository eventRepository, IDistributedCache cache)
    {
        _eventRepository = eventRepository;
        _cache = cache;
    }

    public async Task<List<EventsResponseDTO>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        return await _cache.GetOrCreateAsync("events", async () =>
        {
            return await _eventRepository.GetEventsDTO(cancellationToken);
        });
    }
}