using Digiblob.Api.Auth.Exceptions.Extensions;

namespace Digiblob.Api.Auth.Endpoints;

public sealed class SigninEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication application)
    {
        application.MapPost(ApiRoutes.Posts.Signin, async (
                    HttpRequest request,
                    HttpResponse response,
                    CancellationToken cancellationToken,
                    [FromServices] ISender sender,
                    [FromBody] SigninRequest createUserRequest) =>
                await SigninHandlerAsync(request, response, cancellationToken, sender, createUserRequest))
            .Produces(StatusCodes.Status201Created,typeof(SigninResponse), MediaTypeNames.Application.Json)
            .Produces(StatusCodes.Status422UnprocessableEntity,typeof(ValidationProblemResponse), MediaTypeNames.Application.Json)
            .Produces(StatusCodes.Status500InternalServerError,typeof(ExceptionResponse), MediaTypeNames.Application.Json)
            .AllowAnonymous()
            .WithName($"POST_{nameof(ApiRoutes.Posts.Signin)}")
            .WithDescription($"Creates a new user account and performs sign-in.\n\nEndpoint: POST {ApiRoutes.Posts.Signin}\n\nAccess: Public")
            .WithTags("SIGN-IN", "POST", "V1")
            .WithOpenApi();
    }

    public void DefineServices(IServiceCollection services)
    {
    }
    
    private async Task SigninHandlerAsync(
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
            var body= exception.GetExceptionDescription(locationUri, out var statusCode);
            response.StatusCode = statusCode;
            await response.WriteAsJsonAsync(body, cancellationToken);
        }
    }
}