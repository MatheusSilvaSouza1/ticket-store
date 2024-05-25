using Domain.Organizer.DomainEvents;
using MediatR;

namespace Application.Organizer;

public class OrganizerDomainEventHandler
    : INotificationHandler<OrganizerRegisteredDomainEvent>
{
    public async Task Handle(
        OrganizerRegisteredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        //! Send email
        await Task.CompletedTask;
    }
}