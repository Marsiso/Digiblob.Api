using System.Runtime.Serialization;
using System.Text.Json;

namespace Digiblob.Api.Auth.Models.Get;

[DataContract]
public sealed class SigninResponse
{
    public SigninResponse()
    {
    }

    public SigninResponse(int statusCode, string locationUri)
    {
        this.StatusCode = statusCode;
        this.LocationUri = locationUri;
    }

    [DataMember] public int StatusCode { get; set; }

    [DataMember] public string LocationUri { get; set; } = default!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}