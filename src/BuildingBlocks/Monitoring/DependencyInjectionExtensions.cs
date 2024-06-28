using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Monitoring;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddMonitoring(
        this IServiceCollection services,
        string serviceName)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(trancing =>
            {
                trancing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRedisInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation(e => e.SetDbStatementForText = true)
                    .AddSqlClientInstrumentation(e => e.SetDbStatementForText = true)
                    .AddNpgsql()
                    .AddSource("MassTransit");

                trancing.AddOtlpExporter();
            });

        return services;
    }
}