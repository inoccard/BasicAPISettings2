using BasicAPISettings.Api.Data;
using BasicAPISettings.Api.Domain.Repository;

namespace BasicAPISettings.Api.Configs.Autofac;

public static class RegisterAppServicesConfig
{
    public static void AddAppServices(this IServiceCollection services)
    {
        #region Data

        services.AddScoped<IRepository, DataContext>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        #endregion
    }
}
