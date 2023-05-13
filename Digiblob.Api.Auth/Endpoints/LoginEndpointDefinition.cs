using Digiblob.Api.Auth.Exceptions.Extensions;

namespace Digiblob.Api.Auth.Endpoints;

public sealed class LoginEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication application)
    {
        application.MapPost(ApiRoutes.Posts.Login, async (
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
    
        application.MapGet(ApiRoutes.Gets.Login, async (
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
    }

    public void DefineServices(IServiceCollection services)
    {
    }
    
    private async Task LoginHandlerAsync(
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
            var body = exception.GetExceptionDescription(locationUri, out var statusCode);
            response.StatusCode = statusCode;
            await response.WriteAsJsonAsync(body, cancellationToken);
        }
    }
}