using Digiblob.Api.Auth.Handlers.Commands.Interfaces;
using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Handlers.Commands;

public sealed class SigninHandler : ICommandHandler<SigninCommand, Unit>
{
    private readonly DataContext _dataContext;
    private readonly ILookupNormalizer _lookupNormalizer;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISecurityStampProvider _securityStampProvider;

    public SigninHandler(
        DataContext dataContext,
        IPasswordHasher passwordHasher,
        ILookupNormalizer lookupNormalizer,
        ISecurityStampProvider securityStampProvider)
    {
        this._dataContext = dataContext;
        this._passwordHasher = passwordHasher;
        this._lookupNormalizer = lookupNormalizer;
        this._securityStampProvider = securityStampProvider;
    }

    public async Task<Unit> Handle(SigninCommand request, CancellationToken cancellationToken)
    {
        // Map validated properties
        var userToCreate = request.Adapt<User>();

        // Normalized email and user name
        userToCreate.NormalizedEmail = this._lookupNormalizer.Normalize(request.Email);
        userToCreate.NormalizedUserName = this._lookupNormalizer.Normalize(request.UserName);

        // Security features
        userToCreate.SecurityStamp = this._securityStampProvider.GetStamp();
        userToCreate.PasswordHash = this._passwordHasher.HashPassword(request.Password);

        // Persistence store
        await this._dataContext.Users.AddAsync(userToCreate, cancellationToken);
        await this._dataContext.SaveChangesAsync(cancellationToken);

        // Return
        return new Unit();
    }
}