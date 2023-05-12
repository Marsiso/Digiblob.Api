using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Services;

public sealed class SecurityStampProvider : ISecurityStampProvider
{
    public string GetStamp() => Guid.NewGuid().ToString("D");
}