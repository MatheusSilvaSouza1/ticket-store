using Contracts;

namespace MessageBus;

public interface IMessageBus
{
    Task Publish<T>(T message, CancellationToken cancellationToken = default)
        where T : IntegrationEvent;
}