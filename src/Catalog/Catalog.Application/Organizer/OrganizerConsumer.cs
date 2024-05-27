using Contracts;
using MassTransit;

namespace Catalog.Application.Organizer;

public class OrganizerConsumer
    : IConsumer<OrganizerRegisteredIntegrationEvent>
{
    public Task Consume(ConsumeContext<OrganizerRegisteredIntegrationEvent> context)
    {
        throw new NotImplementedException();
    }
}