using Core.Messages;
using MediatR;

namespace Core.Mediator;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    private readonly IMediator _mediator = mediator;

    public async Task PublishMessage<T>(T message)
        where T : Message
    {
        await _mediator.Publish(message);
    }

    public Task<TResult> SendCommand<T, TResult>(T command, CancellationToken cancellationToken = default)
        where T : Command<TResult>
    {
        return _mediator.Send(command);
    }
}