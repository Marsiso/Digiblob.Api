using Serilog.Events;

namespace Digiblob.Api.Auth.Factories.Extensions;

/// <summary>
///     Extension methods for the logging services factory.
/// </summary>
public static class SerilogLoggerFactoryExtensions
{
    /// <summary>
    ///     Adds default enrichers to the logging service configuration.
    /// </summary>
    /// <param name="configuration">Logging service configuration.</param>
    /// <returns>Logging service configuration.</returns>
    public static LoggerConfiguration SetEnrichers(this LoggerConfiguration configuration) => 
        configuration.Enrich.WithEnvironmentName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()
            .Enrich.FromLogContext();

    /// <summary>
    ///     Adds default sinks to the logging service configuration.
    /// </summary>
    /// <param name="configuration">Logging service configuration.</param>
    /// <returns>Logging service configuration.</returns>
    public static LoggerConfiguration SetSinks(this LoggerConfiguration configuration) =>
        configuration
            .WriteTo.Console(
                restrictedToMinimumLevel: LogEventLevel.Debug,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.Seq(
                serverUrl: "http://localhost:8001",
                restrictedToMinimumLevel: LogEventLevel.Debug);
}