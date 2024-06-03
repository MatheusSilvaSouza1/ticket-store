using Catalog.Domain.Organizer;
using Catalog.Domain.Organizer.Repositories;
using Contracts;
using MassTransit;

namespace Catalog.Application.Promoter;

public class OrganizerConsumer
    : IConsumer<PromoterRegisteredIntegrationEvent>
{
    private readonly IOrganizerRepository _organizerRepository;

    public OrganizerConsumer(IOrganizerRepository organizerRepository)
    {
        _organizerRepository = organizerRepository;
    }

    public async Task Consume(ConsumeContext<PromoterRegisteredIntegrationEvent> context)
    {
        if (context.Message is not null)
        {
            var organizer = Organizers.Create(context.Message.PromoterId, context.Message.PromoterName);

            _organizerRepository.Create(organizer);

            await _organizerRepository.UnitOfWork.CommitAsync();
        }
    }
}