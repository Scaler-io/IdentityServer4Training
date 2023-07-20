using IdentityServer.Configurations.Client;

namespace IdentityServer.DependencyInjections
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var logger = Logging.GetLogger(configuration);
            services.AddSingleton(x => logger);
            services.AddCors();
            return services;
        }
    }
}
