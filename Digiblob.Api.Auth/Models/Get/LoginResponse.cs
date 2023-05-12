using System.Runtime.Serialization;
using System.Text.Json;

namespace Digiblob.Api.Auth.Models.Get;

[DataContract]
public sealed class LoginResponse
{
    [DataMember]
    public string Token { get; set; } = default!;
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}