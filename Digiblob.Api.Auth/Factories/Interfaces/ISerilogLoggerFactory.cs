namespace Digiblob.Api.Auth.Factories.Interfaces;

public interface ISerilogLoggerFactory
{
    /// <summary>
    ///     Creates a logger that logs to the console and requires no additional configuration, services,
    ///     or environment type.
    /// </summary>
    /// <returns>Returns the configured logger instance.</returns>
    ILogger CreateBootstrapLogger();
}