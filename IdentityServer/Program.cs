using IdentityServer.DependencyInjections;
using IdentityServer.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuartion = builder.Configuration;

services.AddApplicationServices(configuartion)
    .AddDatabaseServices(configuartion)
    .AddIdentityServices(configuartion);

var app = builder.Build();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("DevCorsPolicy");

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app
.MigrateDb(configuartion)
.Run();
