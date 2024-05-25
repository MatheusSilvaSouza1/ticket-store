using System.Reflection;

namespace API.Config;

public static class SwaggerConfiguration
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        // services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(x => x.FullName);

            options.ResolveConflictingActions(api => api.First());

            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

            options.IncludeXmlComments(xmlCommentsFullPath);
        });
    }
}