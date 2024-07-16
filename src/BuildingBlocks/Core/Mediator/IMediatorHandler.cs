using Core.Domain;
using Core.Messages;

namespace Core.Mediator;

public interface IMediatorHandler
{
    Task PublishMessage<T>(T message, CancellationToken cancellationToken = default)
        where T : IDomainEvent;

    Task<TResult> SendCommand<T, TResult>(T command, CancellationToken cancellationToken = default)
        where T : ICommand<TResult>;

    Task<TResult> SendQuery<T, TResult>(T query, CancellationToken cancellationToken = default)
        where T : IQuery<TResult>;
}