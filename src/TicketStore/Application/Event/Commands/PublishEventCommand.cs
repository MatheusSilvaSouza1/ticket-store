using Core.Messages;
using ErrorOr;

namespace Application.Event.Commands;

public sealed class PublishEventCommand : ICommand<ErrorOr<Guid>>
{
    public PublishEventCommand(Guid organizerId, Guid eventId, DateTime publishAt)
    {
        OrganizerId = organizerId;
        EventId = eventId;
        PublishAt = publishAt;
    }

    public Guid OrganizerId { get; init; }
    public Guid EventId { get; init; }
    public DateTime PublishAt { get; init; }
}