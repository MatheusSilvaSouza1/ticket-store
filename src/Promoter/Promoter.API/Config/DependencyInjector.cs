using Application.Promoter;
using Application.Promoter.Commands;
using Core.Mediator;
using Domain.Promoter.Repositories;
using ErrorOr;
using Infra;
using Infra.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MessageBus;
using Domain.Promoter.DomainEvents;

namespace API.Config;

public static class DependencyInjector
{
    public static void RegisterDependencyInjector(this IServiceCollection services)
    {

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegisterPromoterCommand, ErrorOr<Guid>>, PromoterCommandHandler>();

        services.AddScoped<INotificationHandler<PromoterRegisteredDomainEvent>, PromoterDomainEventHandler>();

        services.AddScoped<IPromoterRepository, PromoterRepository>();
    }

    public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
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