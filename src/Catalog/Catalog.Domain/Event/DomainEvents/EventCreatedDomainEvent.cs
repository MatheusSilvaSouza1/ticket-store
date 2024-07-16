using Core.Domain;

namespace Domain.Event.DomainEvents;

public class EventCreatedDomainEvent : IDomainEvent
{
    public Guid EventId { get; init; }

    public EventCreatedDomainEvent(Guid eventId)
    {
        EventId = eventId;
    }
}