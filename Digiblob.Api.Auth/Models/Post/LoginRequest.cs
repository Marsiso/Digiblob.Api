using System.Runtime.Serialization;
using System.Text.Json;

namespace Digiblob.Api.Auth.Models.Post;

[DataContract]
public sealed class LoginRequest
{
    [DataMember] public string UserName { get; set; } = default!;

    [DataMember] public string Password { get; set; } = default!;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}