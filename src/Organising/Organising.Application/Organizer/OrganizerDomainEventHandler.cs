using Contracts;
using Domain.Organizer.DomainEvents;
using MediatR;
using MessageBus;

namespace Application.Organizer;

public class OrganizerDomainEventHandler
    : INotificationHandler<OrganizerRegisteredDomainEvent>
{
    private readonly IMessageBus _messageBus;

    public OrganizerDomainEventHandler(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task Handle(
        OrganizerRegisteredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        await _messageBus.Publish(
            new OrganizerRegisteredIntegrationEvent(notification.OrganizerId, notification.OrganizerName),
            cancellationToken);
    }
}