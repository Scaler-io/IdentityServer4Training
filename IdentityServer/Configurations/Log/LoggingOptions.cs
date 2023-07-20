namespace IdentityServer.Configurations.Log
{
    public class LoggingOptions
    {
        public string LogOutputTemplate { get; set; }
        public Console Console { get; set; }
    }

    public class Console
    {
        public bool Enabled { get; set; }
        public string LogLevel { get; set; }
    }
}
