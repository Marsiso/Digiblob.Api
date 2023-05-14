using System.Security.Cryptography;
using System.Text;
using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    private const int KeySize = 32;
    private const int SaltSize = 16;
    private const int Cycles = 1_572_864;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    public string HashPassword(ReadOnlySpan<char> password)
    {
        Span<byte> passwordBytes = stackalloc byte[password.Length];
        Encoding.UTF8.GetBytes(password, passwordBytes);

        Span<byte> salt = stackalloc byte[SaltSize];
        RandomNumberGenerator.Fill(salt);

        Span<byte> key = stackalloc byte[KeySize];
        Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, key, Cycles, Algorithm);

        return $"{Convert.ToBase64String(key)};{Convert.ToBase64String(salt)}";
    }

    public bool VerifyPassword(ReadOnlySpan<char> password, ReadOnlySpan<char> passwordHash)
    {
        Span<byte> passwordBytes = stackalloc byte[password.Length];
        Encoding.UTF8.GetBytes(password, passwordBytes);

        var delimiter = passwordHash.IndexOf(';');
        if (delimiter == -1)
        {
            throw new FormatException("Password hash has no delimiter");
        }

        var passwordHashKey = Convert.FromBase64String(passwordHash[..delimiter++].ToString());
        var passwordHashSalt = Convert.FromBase64String(passwordHash[delimiter..].ToString());

        Span<byte> key = stackalloc byte[KeySize];
        Rfc2898DeriveBytes.Pbkdf2(passwordBytes, passwordHashSalt, key, Cycles, Algorithm);

        return CryptographicOperations.FixedTimeEquals(key, passwordHashKey);
    }
}