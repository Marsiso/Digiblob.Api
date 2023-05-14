using System.Security.Authentication;
using Digiblob.Api.Auth.Handlers.Queries.Interfaces;
using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Handlers.Queries;

public sealed class GetTokenHandler : IQueryHandler<LoginQuery, LoginResponse>
{
    private readonly DataContext _dataContext;
    private readonly ILookupNormalizer _lookupNormalizer;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;

    public GetTokenHandler(DataContext dataContext, ILookupNormalizer lookupNormalizer, IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider)
    {
        this._dataContext = dataContext;
        this._lookupNormalizer = lookupNormalizer;
        this._passwordHasher = passwordHasher;
        this._tokenProvider = tokenProvider;
    }

    public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        // Get user
        var normalizedUserName = this._lookupNormalizer.Normalize(request.UserName);
        var userEntity = await this._dataContext.Users.SingleAsync(
            u => u.NormalizedEmail == normalizedUserName || u.NormalizedUserName == normalizedUserName,
            cancellationToken);

        // Password verification
        if (!this._passwordHasher.VerifyPassword(request.Password, userEntity.PasswordHash))
        {
            throw new AuthenticationException("Invalid user name or password");
        }

        // Return token
        var token = this._tokenProvider.GetToken(userEntity);
        return new LoginResponse(token);
    }
}