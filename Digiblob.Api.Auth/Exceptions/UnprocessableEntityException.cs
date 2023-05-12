using System.Runtime.Serialization;
using System.Text.Json;

namespace Digiblob.Api.Auth.Exceptions;

public sealed class UnprocessableEntityException : Exception
{
    public override string Message { get; }

    public IDictionary<string, string[]> ValidationFailures { get; set; }

    public UnprocessableEntityException(string message, IDictionary<string, string[]> validationFailures)
    {
        Message = message;
        ValidationFailures = validationFailures;
    }
}