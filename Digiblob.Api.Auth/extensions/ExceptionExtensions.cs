using System.Security.Authentication;
using Digiblob.Api.Auth.Models.Get;

namespace Digiblob.Api.Auth.Extensions;

public static class ExceptionExtensions
{
    public static object ExceptionDescriptor(this Exception exception, in string locationUri, out int statusCode)
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
                return new ExceptionResponse(
                    $"Unauthorized exception caught by the exception handler. Endpoint: {locationUri}",
                    exception.Message,
                    statusCode);
            case UnprocessableEntityException unprocessableEntity:
                statusCode = StatusCodes.Status422UnprocessableEntity;
                return new ValidationProblemResponse(
                    $"Unprocessable entity sent from the client. Endpoint: {locationUri}",
                    exception.Message,
                    statusCode,
                    nameof(UnprocessableEntityException),
                    unprocessableEntity.ValidationFailures);
            case OperationCanceledException:
                statusCode = 499;
                return new ExceptionResponse($"Request canceled by the client. Endpoint: {locationUri}",
                    exception.Message,
                    statusCode);
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                return new ExceptionResponse(
                    $"Global exception caught by the exception handler. Endpoint: {locationUri}",
                    exception.Message,
                    statusCode);
        }
    }
}