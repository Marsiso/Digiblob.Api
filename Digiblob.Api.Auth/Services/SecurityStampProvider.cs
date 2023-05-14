using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Services;

public sealed class SecurityStampProvider : ISecurityStampProvider
{
    private const string Format = "D";

    public string GetStamp()
    {
        return Guid.NewGuid().ToString(Format);
    }
}