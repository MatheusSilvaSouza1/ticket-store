using Domain.Organizer.DTOs;
using ErrorOr;
using MediatR;

namespace Application.Organizer.Commands;

public record RegisterOrganizerCommand(CreateOrganizerDTO OrganizerDTO) : IRequest<ErrorOr<Guid>>
{
}