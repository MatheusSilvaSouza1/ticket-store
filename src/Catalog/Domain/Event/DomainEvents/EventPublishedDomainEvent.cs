using Core.Domain;
using Core.Messages;

namespace Domain.Event.DomainEvents;

public class EventPublishedDomainEvent : Message, IDomainEvent
{
    public readonly Guid EventId;
    public readonly DateTime PublishAt;

    public EventPublishedDomainEvent(Guid eventId, DateTime publishAt)
    {
        EventId = eventId;
        PublishAt = publishAt;
    }
}