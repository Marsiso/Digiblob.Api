using System.Runtime.Serialization;
using System.Text.Json;

namespace Digiblob.Api.Auth.Models.Post;

[DataContract]
public sealed class SigninRequest
{
    [DataMember]
    public string? GivenName { get; set; }
    
    [DataMember]
    public string? FamilyName { get; set; }
    
    [DataMember]
    public string? Email { get; set; }
    
    [DataMember]
    public string? UserName { get; set; }
    
    [DataMember]
    public string? Password { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}