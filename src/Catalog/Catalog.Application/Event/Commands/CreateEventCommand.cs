using Core.Messages;
using Domain.Event.DTOs;

namespace Application.Event.Commands;

public sealed class CreateEventCommand : ICommand<ErrorOr<Guid>>
{
    public CreateEventCommand(Guid organizerId, CreateEventDTO eventDTO)
    {
        OrganizerId = organizerId;
        EventDTO = eventDTO;
    }

    public Guid OrganizerId { get; init; }
    public CreateEventDTO EventDTO { get; init; }
}