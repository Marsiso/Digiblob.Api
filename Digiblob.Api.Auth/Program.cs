using System.Net.Mime;
using Digiblob.Api.Auth.Factories;
using Digiblob.Api.Auth.Installers.Extensions;

Log.Logger = new SerilogLoggerFactory().CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateSlimBuilder(args);

    builder.Host.UseSerilog();
    builder.Services.InstallServicesInAssembly(builder.Configuration, builder.Environment);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    var sampleTodos = TodoGenerator.GenerateTodos().ToArray();
    
    var api = app.MapGroup(ApiRoutes.Base);
    api.MapGet("/", () => sampleTodos)
        .Produces(StatusCodes.Status200OK,typeof(Todo[]), MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json)
        .AllowAnonymous();

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