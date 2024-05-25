using Application.Organizer;
using Application.Organizer.Commands;
using Core.Mediator;
using Domain.Organizer.DomainEvents;
using Domain.Organizer.Repositories;
using ErrorOr;
using Infra;
using Infra.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MessageBus;

namespace API.Config;

public static class DependencyInjector
{
    public static void RegisterDependencyInjector(this IServiceCollection services)
    {

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegisterOrganizerCommand, ErrorOr<Guid>>, OrganizerCommandHandler>();

        services.AddScoped<INotificationHandler<OrganizerRegisteredDomainEvent>, OrganizerDomainEventHandler>();

        services.AddScoped<IOrganizerRepository, OrganizerRepository>();
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
        services.AddMessageBus(configuration);
    }
}