using Application.Event.Commands;
using Core.Messages;
using Domain.Event;
using Domain.Event.Repositories;
using Domain.Organizer.Repositories;
using ErrorOr;
using MediatR;

namespace Application.Event;

public class EventHandler : CommandHandler, IRequestHandler<CreateEventCommand, ErrorOr<Guid>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IOrganizerRepository _organizerRepository;

    public EventHandler(IEventRepository eventRepository, IOrganizerRepository organizerRepository)
    {
        _eventRepository = eventRepository;
        _organizerRepository = organizerRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var organizerExists = await _organizerRepository.ExistsAsync(e => e.Id == request.OrganizerId);

        var events = Events.Create(request.OrganizerId, request.EventDTO, organizerExists);

        if (events.IsError)
        {
            return events.Errors;
        }

        _eventRepository.Create(events.Value);

        await _eventRepository.UnitOfWork.CommitAsync();

        return events.Value.Id;
    }
}