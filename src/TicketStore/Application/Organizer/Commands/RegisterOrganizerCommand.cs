using Core.Messages;
using Domain.Organizer.DTOs;
using ErrorOr;

namespace Application.Organizer.Commands;

public class RegisterOrganizerCommand : ICommand<ErrorOr<Guid>>
{
    public RegisterOrganizerDTO OrganizerDTO { get; init; }

    public RegisterOrganizerCommand(RegisterOrganizerDTO organizerDTO)
    {
        OrganizerDTO = organizerDTO;
    }
}