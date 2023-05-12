Log.Logger = new SerilogLoggerFactory().CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateSlimBuilder(args);

    builder.Host.UseSerilog();
    builder.Services.InstallServicesInAssembly(builder.Configuration, builder.Environment);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    var auth = app.MapGroup(string.Empty);
    auth.MapPost(ApiRoutes.Posts.Signin, async (
            HttpRequest request,
            HttpResponse response,
            [FromServices] ISender sender,
            [FromBody] CreateUserRequest createUserRequest) =>
        {
            try
            {
                response.ContentType = MediaTypeNames.Application.Json;
                
                var command = createUserRequest.Adapt<CreateUserCommand>();
                await sender.Send(command);

                return Results.Created();
            }
            catch (Exception exception)
            {
                var baseUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}";
                var locationUri = $"{baseUrl}/{ApiRoutes.Posts.Signin}";
                return exception switch
                {
                    UnprocessableEntityException unprocessableEntityException => Results.ValidationProblem(
                        unprocessableEntityException.ValidationFailures,
                        exception.Message,
                        null, 
                        StatusCodes.Status422UnprocessableEntity,
                        $"Unprocessable entity exception caught by the exception handler. Endpoint: {locationUri}",
                        nameof(UnprocessableEntityException)),
                    _ => Results.Problem(
                        exception.Message,
                        null,
                        StatusCodes.Status500InternalServerError, 
                        $"Global exception caught by the exception handler. Endpoint: {locationUri}",
                        nameof(Exception))
                };
            }
        })
        .Produces(StatusCodes.Status201Created,typeof(IActionResult), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status400BadRequest,typeof(IActionResult), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status422UnprocessableEntity,typeof(IActionResult), MediaTypeNames.Application.Json)
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