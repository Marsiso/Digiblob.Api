using System.Runtime.Serialization;
using System.Text.Json;

namespace Digiblob.Api.Auth.Models.Get;

[DataContract]
public sealed class ValidationProblemResponse
{
    public ValidationProblemResponse()
    {
    }

    public ValidationProblemResponse(string title, string detail, int statusCode,
        IDictionary<string, string[]> validationFailures)
    {
        this.Title = title;
        this.Detail = detail;
        this.StatusCode = statusCode;
        this.ValidationFailures = validationFailures;
    }

    [DataMember] public string Title { get; set; } = default!;

    [DataMember] public string Detail { get; set; } = default!;

    [DataMember] public int StatusCode { get; set; }

    [DataMember] public IDictionary<string, string[]> ValidationFailures { get; set; } = default!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}