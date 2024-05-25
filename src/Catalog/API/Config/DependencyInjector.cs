using Application.Event;
using Application.Event.Commands;
using Application.Event.Queries;
using Application.Organizer;
using Application.Organizer.Commands;
using Core.Mediator;
using Domain.Event.DTOs;
using Domain.Event.Repositories;
using Domain.Organizer.DomainEvents;
using Domain.Organizer.Repositories;
using ErrorOr;
using Infra;
using Infra.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Config;

public static class DependencyInjector
{
    public static void RegisterDependencyInjector(this IServiceCollection services)
    {

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegisterOrganizerCommand, ErrorOr<Guid>>, OrganizerCommandHandler>();
        services.AddScoped<IRequestHandler<CreateEventCommand, ErrorOr<Guid>>, EventCommandHandler>();
        services.AddScoped<IRequestHandler<PublishEventCommand, ErrorOr<Guid>>, EventCommandHandler>();

        services.AddScoped<INotificationHandler<OrganizerRegisteredDomainEvent>, OrganizerDomainEventHandler>();

        services.AddScoped<IRequestHandler<GetEventsQuery, List<EventsResponseDTO>>, EventQueryHandler>();

        services.AddScoped<IOrganizerRepository, OrganizerRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
    }

    public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });
    }

    public static void RegisterMessageBus(this IServiceCollection services)
    {

    }
}