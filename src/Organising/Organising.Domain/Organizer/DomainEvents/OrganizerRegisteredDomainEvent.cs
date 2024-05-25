using Core.Domain;
using Core.Messages;

namespace Domain.Organizer.DomainEvents;

public sealed class OrganizerRegisteredDomainEvent : Message, IDomainEvent
{
    public Guid OrganizerId { get; init; }
    public string OrganizerName { get; init; }

    public OrganizerRegisteredDomainEvent(Guid organizerId, string organizerName)
    {
        OrganizerId = organizerId;
        OrganizerName = organizerName;
    }
}