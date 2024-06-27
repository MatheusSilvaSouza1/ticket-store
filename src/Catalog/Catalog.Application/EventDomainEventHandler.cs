using Domain.Event.DomainEvents;
using Microsoft.Extensions.Caching.Distributed;

namespace Catalog.Application;

public class EventDomainEventHandler
    : INotificationHandler<EventCreatedDomainEvent>
{
    private readonly IDistributedCache _cache;

    public EventDomainEventHandler(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Handle(EventCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync("events", cancellationToken);
    }
}