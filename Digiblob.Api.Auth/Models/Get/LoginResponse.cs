using System.Runtime.Serialization;
using System.Text.Json;

namespace Digiblob.Api.Auth.Models.Get;

[DataContract]
public sealed class LoginResponse
{
    public LoginResponse()
    {
    }

    public LoginResponse(string token)
    {
        this.Token = token;
    }

    [DataMember] public string Token { get; set; } = default!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}