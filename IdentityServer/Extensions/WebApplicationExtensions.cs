using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Configurations.Client;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace IdentityServer.Extensions
{
    public static class ApplicationHOstExtensions
    {
        public static WebApplication MigrateDb(this WebApplication app, IConfiguration configuration)
        {
            using (var scope = app.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>()
                    .Database
                    .Migrate();

                using (var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                        if (!context.Clients.Any())
                        {
                            var clientSettings = configuration.GetSection("ClientSettings").Get<ClientSettings>();
                            foreach (var client in IdentityConfig.Clients(clientSettings))
                                context.Clients.Add(client.ToEntity());
                        }
                        if (!context.IdentityResources.Any())
                        {
                            foreach (var resource in IdentityConfig.IdentityResources)
                                context.IdentityResources.Add(resource.ToEntity());
                        }
                        if (!context.ApiScopes.Any())
                        {
                            foreach (var apiScope in IdentityConfig.ApiScopes)
                                context.ApiScopes.Add(apiScope.ToEntity());
                        }
                        if (!context.ApiResources.Any())
                        {
                            foreach (var apiResource in IdentityConfig.ApiResources)
                                context.ApiResources.Add(apiResource.ToEntity());
                        }
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        logger.Error("error migrating database {@stack}", e.StackTrace);
                        throw;
                    }
                }
            }

            return app;
        }
    }
}