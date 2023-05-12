using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Digiblob.Api.Auth.Services.Interfaces;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;

namespace Digiblob.Api.Auth.Services;

public sealed class TokenProvider : ITokenProvider
{
    public string GetToken(User user)
    {
        var secretKey = new SymmetricSecurityKey("1376c460-6fc0-41d0-b249-a3db7824ad1c"u8.ToArray());
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            issuer: "http://localhost:6000",
            audience: "http://localhost:6000",
            claims: new List<Claim>
            {
                new(JwtClaimTypes.Subject, user.Id.ToString()),
                new(JwtClaimTypes.Email, user.Email),
                new(JwtClaimTypes.GivenName, user.GivenName),
                new(JwtClaimTypes.FamilyName, user.FamilyName)
            },
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: signinCredentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}