var builder = WebApplication.CreateSlimBuilder(args);

builder.Logging.AddConsole();
builder.Services.AddSqlServer<DataContext>(
        "Server=(.);Database=Digiblob;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;",
    option => option.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));

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