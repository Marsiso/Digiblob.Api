using Digiblob.Api.Auth.Installers;
using Digiblob.Api.Auth.Installers.Extensions;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Logging.AddConsole();
builder.Services.InstallServicesInAssembly(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseHttpLogging();
var sampleTodos = TodoGenerator.GenerateTodos().ToArray();

var api = app.MapGroup(ApiRoutes.Base);
api.MapGet("/", () => sampleTodos);
api.MapGet("/{id:int}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.Run();