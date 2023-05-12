using System.Runtime.Serialization;
using System.Text.Json;

namespace Digiblob.Api.Auth.Models.Get;

[DataContract]
public sealed class SigninResponse
{
    [DataMember]
    public int StatusCode { get; set; }

    [DataMember]
    public string LocationUri { get; set; } = default!;

    public SigninResponse()
    {
    }

    public SigninResponse(int statusCode, string locationUri)
    {
        StatusCode = statusCode;
        LocationUri = locationUri;
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}