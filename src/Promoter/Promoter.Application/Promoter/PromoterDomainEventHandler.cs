using Contracts;
using Domain.Promoter.DomainEvents;
using MediatR;
using MessageBus;

namespace Application.Promoter;

public class PromoterDomainEventHandler
    : INotificationHandler<PromoterRegisteredDomainEvent>
{
    private readonly IMessageBus _messageBus;

    public PromoterDomainEventHandler(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task Handle(
        PromoterRegisteredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        await _messageBus.Publish(
            new PromoterRegisteredIntegrationEvent(notification.PromoterId, notification.PromoterName),
            cancellationToken);
    }
}