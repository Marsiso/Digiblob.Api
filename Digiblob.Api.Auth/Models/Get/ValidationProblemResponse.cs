using System.Runtime.Serialization;
using System.Text.Json;

namespace Digiblob.Api.Auth.Models.Get;

[DataContract]
public sealed class ValidationProblemResponse
{
    [DataMember]
    public string Title { get; set; } = default!;

    [DataMember]
    public string Detail { get; set; } = default!;

    [DataMember]
    public int StatusCode { get; set; }

    [DataMember]
    public IDictionary<string, string[]> ValidationFailures { get; set; } = default!;

    public ValidationProblemResponse()
    {
    }

    public ValidationProblemResponse(string title, string detail, int statusCode, IDictionary<string, string[]> validationFailures)
    {
        Title = title;
        Detail = detail;
        StatusCode = statusCode;
        ValidationFailures = validationFailures;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}