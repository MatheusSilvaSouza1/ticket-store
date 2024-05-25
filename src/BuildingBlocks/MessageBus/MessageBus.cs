using Contracts;
using MassTransit;

namespace MessageBus;

public class MessageBus : IMessageBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessageBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Publish<T>(T message, CancellationToken cancellationToken = default)
        where T : IntegrationEvent
    {
        await _publishEndpoint.Publish(message, cancellationToken);
    }
}