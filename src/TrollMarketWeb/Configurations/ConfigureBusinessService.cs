using TrollMarketWeb.APIGateway;

namespace TrollMarketWeb.Configurations;

public static class ConfigureBusinessService
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services){
        services.AddScoped<ConfigureAPI>();
        services.AddScoped<AuthConfigureAPI>();

        return services;
    }
}
