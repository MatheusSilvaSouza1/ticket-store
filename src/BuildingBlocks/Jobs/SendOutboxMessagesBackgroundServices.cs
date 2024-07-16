using Core.Domain;
using Core.Mediator;
using Core.Messages;
using MessageBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Jobs;

public class SendOutboxMessagesBackgroundServices<TContext> : BackgroundService
    where TContext : DbContext
{
    private readonly ILogger<SendOutboxMessagesBackgroundServices<TContext>> _logger;
    private readonly IServiceProvider _serviceProvider;

    public SendOutboxMessagesBackgroundServices(
        ILogger<SendOutboxMessagesBackgroundServices<TContext>> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var _scope = _serviceProvider.CreateScope();
            var _context = _scope.ServiceProvider.GetRequiredService<TContext>();
            var _mediatorHandler = _scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

            var messages = await _context.Set<OutboxMessage>()
                .Where(e => e.ProcessedOnUtc == null)
                .Take(10)
                .ToListAsync(stoppingToken);

            if (messages.Count == 0)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }

            foreach (var message in messages)
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                        message.Content,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });

                if (domainEvent is null)
                {
                    _logger.LogError("Domain event null event: {domainEvent}", JsonConvert.SerializeObject(domainEvent));
                    continue;
                }

                await _mediatorHandler.PublishMessage(domainEvent, stoppingToken);

                message.ProcessedOnUtc = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(stoppingToken);
        }
    }
}