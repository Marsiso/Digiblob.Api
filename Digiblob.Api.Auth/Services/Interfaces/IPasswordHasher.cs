namespace Digiblob.Api.Auth.Services.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(ReadOnlySpan<char> password);
    bool VerifyPassword(ReadOnlySpan<char> password, ReadOnlySpan<char> passwordHash);
}