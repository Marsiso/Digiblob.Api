using System.Security.Authentication;

namespace Digiblob.Api.Auth.Exceptions.Extensions;

public static class ExceptionExtensions
{
    public static object GetExceptionDescription(this Exception exception, in string locationUri, out int statusCode)
    {
        switch (exception)
        {
            case BadRequestException:
                statusCode = StatusCodes.Status400BadRequest;
                return new ExceptionResponse($"Object sent from the client is null. Endpoint: {locationUri}",
                    exception.Message,
                    statusCode);
            case AuthenticationException:
                statusCode = StatusCodes.Status401Unauthorized;
                return new ExceptionResponse($"Credentials sent from the client are invalid. Endpoint: {locationUri}",
                    exception.Message,
                    statusCode);
            case UnprocessableEntityException unprocessableEntity:
                statusCode = StatusCodes.Status422UnprocessableEntity;
                return new ValidationProblemResponse($"Object sent from the client is invalid. Endpoint: {locationUri}",
                    exception.Message,
                    statusCode,
                    unprocessableEntity.ValidationFailures);
            case OperationCanceledException:
                statusCode = 499;
                return new ExceptionResponse($"Request canceled by the client. Endpoint: {locationUri}",
                    exception.Message,
                    statusCode);
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                return new ExceptionResponse($"Global exception caught by the exception handler. Endpoint: {locationUri}",
                    exception.Message,
                    statusCode);
        }
    }
}