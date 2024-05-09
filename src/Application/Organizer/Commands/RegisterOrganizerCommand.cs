using Core.Messages;
using Domain.Organizer.DTOs;
using ErrorOr;

namespace Application.Organizer.Commands;

public class RegisterOrganizerCommand(RegisterOrganizerDTO organizerDTO) : Command<ErrorOr<Guid>>
{
    public RegisterOrganizerDTO OrganizerDTO { get; init; } = organizerDTO;
}