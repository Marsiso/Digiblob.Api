using System.Security.Authentication;

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

                return Results.CreatedAtRoute($"GET_{nameof(ApiRoutes.Gets.Login)}", new { });
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
        .Produces(StatusCodes.Status201Created,typeof(Results), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status422UnprocessableEntity,typeof(Results), MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json)
        .AllowAnonymous()
        .WithName($"POST_{nameof(ApiRoutes.Posts.Signin)}");
    
    auth.MapPost(ApiRoutes.Posts.Login, async (
            HttpRequest request,
            HttpResponse response,
            [FromServices] ISender sender,
            [FromBody] LoginRequest loginRequest) =>
        {
            try
            {
                response.ContentType = MediaTypeNames.Application.Json;
                
                var query = loginRequest.Adapt<LoginQuery>();
                var result = await sender.Send(query);

                return Results.Ok(result);
            }
            catch (Exception exception)
            {
                var baseUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}";
                var locationUri = $"{baseUrl}/{ApiRoutes.Posts.Login}";
                return exception switch
                {
                    AuthenticationException => Results.Unauthorized(),
                    UnprocessableEntityException => Results.Unauthorized(),
                    _ => Results.Problem(
                        exception.Message,
                        null,
                        StatusCodes.Status500InternalServerError, 
                        $"Global exception caught by the exception handler. Endpoint: {locationUri}",
                        nameof(Exception))
                };
            }
        })
        .Produces(StatusCodes.Status200OK,typeof(Results), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status401Unauthorized,typeof(Results), MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json)
        .AllowAnonymous()
        .WithName($"POST_{nameof(ApiRoutes.Posts.Login)}");
    
    auth.MapGet(ApiRoutes.Gets.Login, async (
            HttpRequest request,
            HttpResponse response,
            [FromServices] ISender sender,
            [FromBody] LoginRequest loginRequest) =>
        {
            try
            {
                response.ContentType = MediaTypeNames.Application.Json;
                
                var query = loginRequest.Adapt<LoginQuery>();
                var result = await sender.Send(query);

                return Results.Ok(result);
            }
            catch (Exception exception)
            {
                var baseUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}";
                var locationUri = $"{baseUrl}/{ApiRoutes.Posts.Login}";
                return exception switch
                {
                    AuthenticationException => Results.Unauthorized(),
                    UnprocessableEntityException => Results.Unauthorized(),
                    _ => Results.Problem(
                        exception.Message,
                        null,
                        StatusCodes.Status500InternalServerError, 
                        $"Global exception caught by the exception handler. Endpoint: {locationUri}",
                        nameof(Exception))
                };
            }
        })
        .Produces(StatusCodes.Status200OK,typeof(Results), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status401Unauthorized,typeof(Results), MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json)
        .AllowAnonymous()
        .WithName($"GET_{nameof(ApiRoutes.Gets.Login)}");

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