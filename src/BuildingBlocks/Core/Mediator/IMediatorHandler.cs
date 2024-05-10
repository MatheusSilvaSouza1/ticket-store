using Core.Messages;

namespace Core.Mediator;

public interface IMediatorHandler
{
    Task PublishMessage<T>(T message)
        where T : Event;

    Task<TResult> SendCommand<T, TResult>(T command, CancellationToken cancellationToken = default)
        where T : Command<TResult>;
}