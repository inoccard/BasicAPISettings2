using IdentityServer4.AccessTokenValidation;
using System.Security.Claims;

namespace BasicAPISettings.Api.Configs.Auth;

public static class AuthenticationConfig
{
    public static void AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = configuration["OAuth:Authority"];
                options.ApiName = configuration["OAuth:ApiName"];
                options.ApiSecret = configuration["OAuth:ApiSecret"];
                options.CacheDuration = TimeSpan.FromMinutes(configuration.GetValue<int>("OAuth:CacheDuration"));
                options.EnableCaching = true;
                options.RoleClaimType = ClaimTypes.Role;
            });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public static void UseIdentityConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
