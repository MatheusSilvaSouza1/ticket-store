using Core.Domain;

namespace Domain.Event.DomainEvents;

public class EventPublishedDomainEvent : IDomainEvent
{
    public readonly Guid EventId;
    public readonly DateTime PublishAt;

    public EventPublishedDomainEvent(Guid eventId, DateTime publishAt)
    {
        EventId = eventId;
        PublishAt = publishAt;
    }
}