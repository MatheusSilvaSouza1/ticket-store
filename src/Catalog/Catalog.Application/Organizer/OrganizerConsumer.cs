using Catalog.Domain.Organizer;
using Catalog.Domain.Organizer.Repositories;
using Contracts;
using MassTransit;

namespace Catalog.Application.Organizer;

public class OrganizerConsumer
    : IConsumer<OrganizerRegisteredIntegrationEvent>
{
    private readonly IOrganizerRepository _organizerRepository;

    public OrganizerConsumer(IOrganizerRepository organizerRepository)
    {
        _organizerRepository = organizerRepository;
    }

    public async Task Consume(ConsumeContext<OrganizerRegisteredIntegrationEvent> context)
    {
        if (context.Message is not null)
        {
            var organizer = Organizers.Create(context.Message.OrganizerId, context.Message.OrganizerName);

            _organizerRepository.Create(organizer);

            await _organizerRepository.UnitOfWork.CommitAsync();
        }
    }
}