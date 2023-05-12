using System.Security.Cryptography;
using System.Text;
using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    public string HashPassword(ReadOnlySpan<char> password)
    {
        Span<byte> passwordBytes = stackalloc byte[password.Length];
        Encoding.UTF8.GetBytes(password, passwordBytes);

        Span<byte> salt = stackalloc byte[16];
        RandomNumberGenerator.Fill(salt);
        
        Span<byte> key = stackalloc byte[32];
        Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, key, 1_572_864, HashAlgorithmName.SHA512);

        return $"{Convert.ToBase64String(key)};{Convert.ToBase64String(salt)}";
    }

    public bool VerifyPassword(ReadOnlySpan<char> password, ReadOnlySpan<char> passwordHash)
    {
        Span<byte> passwordBytes = stackalloc byte[password.Length];
        Encoding.UTF8.GetBytes(password, passwordBytes);

        var delimiter = passwordHash.IndexOf(';');
        if (delimiter == -1) throw new FormatException("Password hash has no delimiter");

        var passwordHashKey = Convert.FromBase64String(passwordHash[..delimiter++].ToString());
        var passwordHashSalt = Convert.FromBase64String(passwordHash[delimiter..].ToString());
        
        Span<byte> key = stackalloc byte[32];
        Rfc2898DeriveBytes.Pbkdf2(passwordBytes, passwordHashSalt, key, 512_000, HashAlgorithmName.SHA512);

        return CryptographicOperations.FixedTimeEquals(key, passwordHashKey);
    }
}