using Core.Messages;
using Domain.Event.DTOs;
using ErrorOr;

namespace Application.Event.Commands;

public class CreateEventCommand : Command<ErrorOr<Guid>>
{
    public CreateEventCommand(Guid organizerId, CreateEventDTO eventDTO)
    {
        OrganizerId = organizerId;
        EventDTO = eventDTO;
    }

    public Guid OrganizerId { get; init; }
    public CreateEventDTO EventDTO { get; init; }
}