namespace Digiblob.Api.Auth.Services.Interfaces;

public interface ITokenProvider
{
    string GetToken(User user);
}