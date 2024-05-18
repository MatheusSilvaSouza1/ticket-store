using Core.Domain;

namespace Domain.Organizer.DomainEvents;

public sealed class OrganizerRegisteredDomainEvent : Core.Messages.Event, IDomainEvent
{
    public Guid OrganizerId { get; init; }

    public OrganizerRegisteredDomainEvent(Guid organizerId)
    {
        OrganizerId = organizerId;
    }
}