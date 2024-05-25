using Core.Domain;
using Core.Messages;

namespace Domain.Event.DomainEvents;

public class EventCreatedDomainEvent : Message, IDomainEvent
{
    public Guid EventId { get; init; }

    public EventCreatedDomainEvent(Guid eventId)
    {
        EventId = eventId;
    }
}