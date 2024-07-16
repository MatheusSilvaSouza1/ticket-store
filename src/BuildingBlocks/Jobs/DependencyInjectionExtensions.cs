using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jobs;

public static class DependencyInjectionExtensions
{
    public static void AddOutboxSendMessages<TContext>(
        this IServiceCollection services)
        where TContext : DbContext
    {
        services.Configure<HostOptions>(e =>
        {
            e.ServicesStartConcurrently = false;
            e.ServicesStopConcurrently = false;
        })
        .AddHostedService<SendOutboxMessagesBackgroundServices<TContext>>();
    }
}