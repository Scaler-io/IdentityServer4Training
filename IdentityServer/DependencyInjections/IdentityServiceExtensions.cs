using System.Reflection;
using IdentityServer.Configurations.Client;
using IdentityServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.DependencyInjections
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var clientSettings = configuration.GetSection("ClientSettings").Get<ClientSettings>();

            var builder = services.AddIdentityServer(options =>
            {
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = c =>
                c.UseSqlServer(
                    configuration.GetConnectionString("IdentityServer"),
                    sql => sql.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().ToString())
                );
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = o =>
                o.UseSqlServer(
                    configuration.GetConnectionString("IdentityServer"),
                    sql => sql.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().ToString())
                );
            })
            .AddAspNetIdentity<User>()
            .AddDeveloperSigningCredential();
            return services;
        }
    }
}
