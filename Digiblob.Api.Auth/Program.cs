using Digiblob.Api.Auth.Extensions;
using Digiblob.Api.Auth.Middlewares.Extensions;
using Digiblob.Api.Auth.Models.Get;

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

    var auth = app.MapGroup(string.Empty);
    
    /*
     * Creates a new user account and performs sign-in.
     *
     * Endpoint: POST api/v1/signin
     * Access: Public
     * Tags: SIGN-IN, POST, V1
     */
    auth.MapPost(ApiRoutes.Posts.Signin, async (
            HttpRequest request,
            HttpResponse response,
            CancellationToken cancellationToken,
            [FromServices] ISender sender,
            [FromBody] SigninRequest createUserRequest) =>
            await SigninHandlerAsync(request, response, cancellationToken, sender, createUserRequest).ConfigureAwait(false))
        .Produces(StatusCodes.Status201Created,typeof(SigninResponse), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status422UnprocessableEntity,typeof(ValidationProblemResponse), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status500InternalServerError,typeof(ExceptionResponse), MediaTypeNames.Application.Json)
        .AllowAnonymous()
        .WithName($"POST_{nameof(ApiRoutes.Posts.Signin)}")
        .WithDescription($"Creates a new user account and performs sign-in.\n\nEndpoint: POST {ApiRoutes.Posts.Signin}\n\nAccess: Public")
        .WithTags("SIGN-IN", "POST", "V1")
        .WithOpenApi();
    
    /*
     * Retrieves user login information.
     *
     * Endpoint: POST api/v1/login
     * Access: Public
     * Tags: LOGIN, POST, V1
     */
    auth.MapPost(ApiRoutes.Posts.Login, async (
            HttpRequest request,
            HttpResponse response,
            CancellationToken cancellationToken,
            [FromServices] ISender sender,
            [FromBody] LoginRequest loginRequest) =>
            await LoginHandlerAsync(request, response, cancellationToken, sender, loginRequest))
        .Produces(StatusCodes.Status200OK,typeof(LoginResponse), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status401Unauthorized,typeof(ExceptionResponse), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status500InternalServerError,typeof(ExceptionResponse), MediaTypeNames.Application.Json)
        .AllowAnonymous()
        .WithName($"POST_{nameof(ApiRoutes.Posts.Login)}")
        .WithDescription($"Retrieves user login information.\n\nEndpoint: POST {ApiRoutes.Posts.Login}\n\nAccess: Public")
        .WithTags("LOGIN", "POST", "V1")
        .WithOpenApi();
    
    /*
     * Retrieves user login information.
     *
     * Endpoint: GET api/v1/login
     * Access: Public
     * Tags: LOGIN, GET, V1
     */
    auth.MapGet(ApiRoutes.Gets.Login, async (
            HttpRequest request,
            HttpResponse response,
            CancellationToken cancellationToken,
            [FromServices] ISender sender,
            [FromBody] LoginRequest loginRequest) =>
            await LoginHandlerAsync(request, response, cancellationToken, sender, loginRequest))
        .Produces(StatusCodes.Status200OK,typeof(LoginResponse), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status401Unauthorized,typeof(ExceptionResponse), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status500InternalServerError,typeof(ExceptionResponse), MediaTypeNames.Application.Json)
        .AllowAnonymous()
        .WithName($"GET_{nameof(ApiRoutes.Gets.Login)}")
        .WithDescription($"Retrieves user login information.\n\nEndpoint: GET {ApiRoutes.Gets.Login}\n\nAccess: Public")
        .WithTags("LOGIN", "GET", "V1")
        .WithOpenApi();

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

async Task SigninHandlerAsync(
    HttpRequest request,
    HttpResponse response,
    CancellationToken cancellationToken,
    [FromServices] ISender sender,
    [FromBody] SigninRequest createUserRequest)
{
    try
    {
        // Response Content Type
        response.ContentType = MediaTypeNames.Application.Json;
        
        // Command
        var command = createUserRequest.Adapt<SigninCommand>();
        await sender.Send(command, cancellationToken);

        // URI
        var baseUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}";
        var locationUri = $"{baseUrl}/{ApiRoutes.Posts.Login}";
        
        // Response
        response.StatusCode = StatusCodes.Status201Created;
        await response.WriteAsJsonAsync(new SigninResponse(StatusCodes.Status201Created, locationUri),
            cancellationToken);
    }
    catch (Exception exception)
    {
        // URI
        var baseUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}";
        var locationUri = $"{baseUrl}/{ApiRoutes.Posts.Signin}";
        
        // Response
        var body= exception.ExceptionDescriptor(locationUri, out var statusCode);
        response.StatusCode = statusCode;
        await response.WriteAsJsonAsync(body, cancellationToken);
    }
}

async Task LoginHandlerAsync(
    HttpRequest request,
    HttpResponse response,
    CancellationToken cancellationToken,
    [FromServices] ISender sender,
    [FromBody] LoginRequest loginRequest)
{
    try
    {
        // Response Content Type
        response.ContentType = MediaTypeNames.Application.Json;
        
        // Query
        var query = loginRequest.Adapt<LoginQuery>();
        var result = await sender.Send(query, cancellationToken);
        
        // Response
        response.StatusCode = StatusCodes.Status200OK;
        await response.WriteAsJsonAsync(result, cancellationToken);
    }
    catch (Exception exception)
    {
        // URI
        var baseUrl = $"{request.Scheme}://{request.Host.ToUriComponent()}";
        var locationUri = $"{baseUrl}/{ApiRoutes.Posts.Login}";
        
        // Response
        var body = exception.ExceptionDescriptor(locationUri, out var statusCode);
        response.StatusCode = statusCode;
        await response.WriteAsJsonAsync(body, cancellationToken);
    }
}