using Core.Messages;
using Domain.Event.DTOs;

namespace Application.Event.Queries;

public class GetEventsQuery : IQuery<List<EventsResponseDTO>>
{
}