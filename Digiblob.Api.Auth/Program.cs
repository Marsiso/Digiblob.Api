using Digiblob.Api.Auth.Endpoints.Extensions;

Log.Logger = new SerilogLoggerFactory().CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateSlimBuilder(args);

    builder.Host.UseSerilog();
    builder.Services.InstallServicesInAssembly(builder.Configuration, builder.Environment);

    var app = builder.Build();

    app.UseSerilogRequestLogging();
    app.ConfigureSwagger(app.Configuration, app.Environment);
    app.UseHttpsRedirection();
    app.UseEndpointDefinitions();
    
    Log.Information("Auth API is starting up");
    app.Run();
}
catch (Exception exception)
{
    Log.Error(exception, "Auth API encountered errors. Application is shutting down");
}
finally
{
    Log.CloseAndFlush();
}