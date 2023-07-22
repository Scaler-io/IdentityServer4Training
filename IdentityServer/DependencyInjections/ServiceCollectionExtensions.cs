namespace IdentityServer.DependencyInjections
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var logger = Logging.GetLogger(configuration);
            services.AddSingleton(x => logger);
            services.AddCors(options =>
            {
                options.AddPolicy("DevCorsPolicy", policy =>
                {
                    policy.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod() ;
                });
            });

            services.AddControllersWithViews();
            return services;
        }
    }
}
