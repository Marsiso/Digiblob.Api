using Digiblob.Api.Auth.Installers;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Logging.AddConsole();
builder.Services.InstallServicesInAssembly(builder.Configuration, builder.Environment);
var app = builder.Build();

app.UseHttpLogging();
var sampleTodos = TodoGenerator.GenerateTodos().ToArray();

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.Run();