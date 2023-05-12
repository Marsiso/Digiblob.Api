namespace Digiblob.Api.Auth.Models.Post;

public sealed class LoginRequest
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}