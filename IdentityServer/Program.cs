using IdentityServer.DependencyInjections;
using IdentityServer.Models.Enums;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuartion = builder.Configuration;

services.AddApplicationServices(configuartion)
    .AddIdentityServices(configuartion);

var app = builder.Build();

app.MapGet("/", () =>
{
    using (var scope = app.Services.CreateScope())
    {
        var service = scope.ServiceProvider;
        var logger = service.GetRequiredService<Serilog.ILogger>();

        var c = ApprovedScopes.MovieApi;

        logger.Information("logger working {@scope}", c);
        return "Hello World!";
    }
});

app.UseIdentityServer();

app.UseCors(policy =>
{
    policy.AllowAnyOrigin();
});

app.Run();
