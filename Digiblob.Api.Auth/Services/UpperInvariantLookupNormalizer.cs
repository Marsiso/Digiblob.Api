using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Services;

public sealed class UpperInvariantLookupNormalizer : ILookupNormalizer
{
    public string Normalize(string userName) => userName.Normalize().ToUpperInvariant();
}