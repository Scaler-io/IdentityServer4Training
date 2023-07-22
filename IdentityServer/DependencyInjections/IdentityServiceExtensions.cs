using IdentityServer.Configurations.Client;

namespace IdentityServer.DependencyInjections
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var clientSettings = configuration.GetSection("ClientSettings").Get<ClientSettings>();

            services.AddIdentityServer()
               .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
               .AddInMemoryClients(IdentityConfig.Clients(clientSettings))
               .AddTestUsers(IdentityConfig.TestUsers)
               .AddInMemoryApiResources(IdentityConfig.ApiResources)
               .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
               .AddDeveloperSigningCredential();
            return services;
        }
    }
}
