using Application.Event.Commands;
using Core.Messages;
using Domain.Event;
using Domain.Event.Repositories;

namespace Application.Event;

public class EventCommandHandler : CommandHandler,
    IRequestHandler<CreateEventCommand, ErrorOr<Guid>>,
    IRequestHandler<PublishEventCommand, ErrorOr<Guid>>
{
    private readonly IEventRepository _eventRepository;

    public EventCommandHandler(
        IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(
        CreateEventCommand request,
        CancellationToken cancellationToken)
    {
        // var organizerExists = await _organizerRepository
        //     .ExistsAsync(e => e.Id == request.OrganizerId);
        var organizerExists = true;

        var events = Events.Create(request.OrganizerId, request.EventDTO, organizerExists);

        if (events.IsError)
        {
            return events.Errors;
        }

        _eventRepository.Create(events.Value);

        await _eventRepository.UnitOfWork.CommitAsync();

        return events.Value.Id;
    }

    public async Task<ErrorOr<Guid>> Handle(
        PublishEventCommand request,
        CancellationToken cancellationToken)
    {
        var eventDomain = await _eventRepository.FindEvent(
            request.OrganizerId,
            request.EventId,
            cancellationToken);

        if (eventDomain is null)
        {
            return EventsErrors.EventDoesNotExist;
        }

        var result = eventDomain.PublishEvent(request.PublishAt);

        if (result.Count > 0)
        {
            return result;
        }

        await _eventRepository.UnitOfWork.CommitAsync(cancellationToken);
        return ErrorOrFactory.From(eventDomain.Id);
    }
}