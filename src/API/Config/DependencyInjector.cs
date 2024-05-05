using Application.Organizer;
using Application.Organizer.Commands;
using ErrorOr;
using MediatR;

namespace API.Config;

public static class DependencyInjector
{
    public static void RegisterDependencyInjector(this IServiceCollection services)
    {
        services.AddCors();

        services.AddScoped<IRequestHandler<RegisterOrganizerCommand, ErrorOr<Guid>>, OrganizerHandler>();
    }

    public static void RegisterDatabase(this IServiceCollection services)
    {

    }

    public static void RegisterMessageBus(this IServiceCollection services)
    {

    }
}