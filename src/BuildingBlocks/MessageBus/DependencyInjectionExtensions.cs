using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessageBus;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(builder =>
        {
            builder.SetKebabCaseEndpointNameFormatter();

            builder.UsingRabbitMq((context, config) =>
                {
                    var host = configuration.GetRequiredSection("MessageHost:Host").Value ?? throw new ArgumentException();
                    config.Host(new Uri(host), e =>
                    {
                        e.Username(configuration.GetRequiredSection("MessageHost:User").Value!);
                        e.Password(configuration.GetRequiredSection("MessageHost:Pass").Value!);
                    });

                    config.ConfigureEndpoints(context);
                });
        });

        services.AddScoped<IMessageBus, MessageBus>();

        return services;
    }
}