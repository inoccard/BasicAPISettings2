using BasicAPISettings.Api.Data;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

namespace BasicAPISettings.Api.Configs;

public static class AppConfiguration
{
    /// <summary>
    /// Configura a Api e sua integridade (HealthChecks)
    /// </summary>
    /// <param name="services"></param>
    public static void AddAppConfiguration(this IServiceCollection services)
    {
        services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>())
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddLocalization(options => options.ResourcesPath = "");

        services.AddHealthChecks().AddDbContextCheck<DataContext>();
    }
}
