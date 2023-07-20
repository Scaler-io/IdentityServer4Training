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
               .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
               .AddInMemoryApiResources(IdentityConfig.ApiResources)
               .AddDeveloperSigningCredential();
            return services;
        }
    }
}
