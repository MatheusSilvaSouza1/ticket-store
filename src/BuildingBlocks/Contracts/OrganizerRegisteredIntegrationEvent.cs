namespace Contracts;

public record OrganizerRegisteredIntegrationEvent(
    Guid OrganizerId,
    string OrganizerName) : IntegrationEvent
{
}