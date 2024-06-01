using Application.Event.Queries;
using Domain.Event.DTOs;
using Domain.Event.Repositories;

namespace Application.Event;

public class EventQueryHandler :
    IRequestHandler<GetEventsQuery, List<EventsResponseDTO>>
{
    private readonly IEventRepository _eventRepository;

    public EventQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<List<EventsResponseDTO>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        return await _eventRepository.GetEventsDTO(cancellationToken);
    }
}