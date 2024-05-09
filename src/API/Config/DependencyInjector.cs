using Application.Organizer;
using Application.Organizer.Commands;
using ErrorOr;
using Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Config;

public static class DependencyInjector
{
    public static void RegisterDependencyInjector(this IServiceCollection services)
    {

        services.AddScoped<IRequestHandler<RegisterOrganizerCommand, ErrorOr<Guid>>, OrganizerHandler>();
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