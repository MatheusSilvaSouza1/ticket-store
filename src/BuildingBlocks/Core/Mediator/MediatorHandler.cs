using Core.Domain;
using Core.Messages;
using MediatR;

namespace Core.Mediator;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    private readonly IMediator _mediator = mediator;

    public async Task PublishMessage<T>(T message, CancellationToken cancellationToken = default)
        where T : IDomainEvent
    {
        await _mediator.Publish(message);
    }

    public Task<TResult> SendCommand<T, TResult>(T command, CancellationToken cancellationToken = default)
        where T : ICommand<TResult>
    {
        return _mediator.Send(command);
    }

    public Task<TResult> SendQuery<T, TResult>(T query, CancellationToken cancellationToken = default) where T : IQuery<TResult>
    {
        return _mediator.Send(query);
    }
}