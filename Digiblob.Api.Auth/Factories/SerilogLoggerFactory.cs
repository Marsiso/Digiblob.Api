using Digiblob.Api.Auth.Factories.Extensions;
using Digiblob.Api.Auth.Factories.Interfaces;
using Serilog.Events;

namespace Digiblob.Api.Auth.Factories;

/// <summary>
///     Factory for creating logging service instances.
/// </summary>
public sealed class SerilogLoggerFactory : ISerilogLoggerFactory
{
    /// <summary>
    ///     Parameterless constructor for the host bootstrapping logger.
    /// </summary>
    public SerilogLoggerFactory()
    {
    }

    /// <summary>
    ///     Creates a logger that logs to the console and requires no additional configuration, services,
    ///     or environment type.
    /// </summary>
    /// <returns>Returns the configured logger instance.</returns>
    public ILogger CreateBootstrapLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .SetSinks()
            .SetEnrichers()
            .CreateLogger();
    }
}