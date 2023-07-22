using Destructurama;
using IdentityServer.Configurations.AppConfig;
using IdentityServer.Configurations.Log;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace IdentityServer
{
    public class Logging
    {
        public static ILogger GetLogger(IConfiguration configuration)
        {
            var loggingOptions = configuration.GetSection("Logging").Get<LoggingOptions>();
            var appConfiguration = configuration.GetSection("AppConfiguration").Get<AppConfiguration>();

            Enum.TryParse(loggingOptions.Console.LogLevel, false, out LogEventLevel minimumEventLevel);

            var loggerConfiguration = new LoggerConfiguration()
                //.MinimumLevel.ControlledBy(new LoggingLevelSwitch(minimumEventLevel))
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty(nameof(Environment.MachineName), Environment.MachineName)
                .Enrich.WithProperty(nameof(appConfiguration.ApplicationIdentifier), appConfiguration.ApplicationEnvironment)
                .Enrich.WithProperty(nameof(appConfiguration.ApplicationEnvironment), appConfiguration.ApplicationEnvironment);

            if (loggingOptions.Console.Enabled)
            {
                loggerConfiguration.WriteTo.Console(minimumEventLevel, loggingOptions.LogOutputTemplate);
            }

            return loggerConfiguration
                   .Destructure
                   .UsingAttributes()
                   .CreateLogger();
        }
    }
}
