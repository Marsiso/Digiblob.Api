namespace Digiblob.Api.Auth.Exceptions;

public sealed class UnprocessableEntityException : Exception
{
    public UnprocessableEntityException(string message, IDictionary<string, string[]> validationFailures)
    {
        this.Message = message;
        this.ValidationFailures = validationFailures;
    }

    public override string Message { get; }

    public IDictionary<string, string[]> ValidationFailures { get; set; }
}