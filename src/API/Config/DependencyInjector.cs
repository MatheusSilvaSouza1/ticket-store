using Application.Organizer;
using Application.Organizer.Commands;
using Core.Mediator;
using ErrorOr;
using Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Config;

public static class DependencyInjector
{
    public static void RegisterDependencyInjector(this IServiceCollection services)
    {
        services.AddCors();

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegisterOrganizerCommand, ErrorOr<Guid>>, OrganizerHandler>();
    }

    public static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            options.EnableDetailedErrors();
        });
    }

    public static void RegisterMessageBus(this IServiceCollection services)
    {

    }
}