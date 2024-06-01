using Application.Event;
using Application.Event.Commands;
using Application.Event.Queries;
using Core.Mediator;
using Domain.Event.DTOs;
using Domain.Event.Repositories;
using ErrorOr;
using Infra;
using Infra.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MessageBus;
using Catalog.Application.Organizer;
using Catalog.Domain.Organizer.Repositories;

namespace API.Config;

public static class DependencyInjector
{
    public static void RegisterDependencyInjector(this IServiceCollection services)
    {

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<CreateEventCommand, ErrorOr<Guid>>, EventCommandHandler>();
        services.AddScoped<IRequestHandler<PublishEventCommand, ErrorOr<Guid>>, EventCommandHandler>();

        services.AddScoped<IRequestHandler<GetEventsQuery, List<EventsResponseDTO>>, EventQueryHandler>();

        services.AddScoped<IEventRepository, EventRepository>();
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
        services.AddMessageBus(
            configuration,
            consumers: [
                typeof(OrganizerConsumer)]);
    }
}