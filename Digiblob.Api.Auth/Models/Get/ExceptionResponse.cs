using System.Runtime.Serialization;
using System.Text.Json;

namespace Digiblob.Api.Auth.Models.Get;

[DataContract]
public sealed class ExceptionResponse
{
    public ExceptionResponse()
    {
    }

    public ExceptionResponse(string title, string detail, int statusCode)
    {
        this.Title = title;
        this.Detail = detail;
        this.StatusCode = statusCode;
    }

    [DataMember] public string Title { get; set; } = default!;

    [DataMember] public string Detail { get; set; } = default!;

    [DataMember] public int StatusCode { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}