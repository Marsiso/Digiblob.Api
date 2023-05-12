using System.Security.Authentication;
using Digiblob.Api.Auth.Handlers.Queries.Interfaces;
using Digiblob.Api.Auth.Models.Get;
using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Handlers.Queries;

public sealed class GetTokenHandler : IQueryHandler<LoginQuery, LoginResponse>
{
    private readonly DataContext _dataContext;
    private readonly ILookupNormalizer _lookupNormalizer;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    
    public GetTokenHandler(DataContext dataContext, ILookupNormalizer lookupNormalizer, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
    {
        _dataContext = dataContext;
        _lookupNormalizer = lookupNormalizer;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
    }
    
    public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var normalizedUserName = _lookupNormalizer.Normalize(request.UserName);
        var userEntity = await _dataContext.Users.SingleAsync(
            u => u.NormalizedEmail == normalizedUserName || u.NormalizedUserName == normalizedUserName,
            cancellationToken: cancellationToken);

        if (!_passwordHasher.VerifyPassword(request.Password, userEntity.PasswordHash))
        {
            throw new AuthenticationException("Invalid user name or password");
        }

        return new LoginResponse
        {
            Token = _tokenProvider.GetToken(userEntity)
        };
    }
}