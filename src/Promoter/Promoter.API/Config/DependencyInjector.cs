namespace API.Config;

public static class DependencyInjector
{
    public static void RegisterDependencyInjector(this IServiceCollection services)
    {

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegisterPromoterCommand, Result<Guid>>, PromoterCommandHandler>();

        services.AddScoped<INotificationHandler<PromoterRegisteredDomainEvent>, PromoterDomainEventHandler>();

        services.AddScoped<IPromoterRepository, PromoterRepository>();
    }

    public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessageInterceptor>();

        services.AddDbContext<Context>((sp, options) =>
            {
                var interceptor = sp.GetRequiredService<ConvertDomainEventsToOutboxMessageInterceptor>();

                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                    .AddInterceptors(interceptor);

                options.EnableSensitiveDataLogging();
            });
    }

    public static void RegisterMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBus(
            configuration,
            consumers: []);
    }
}