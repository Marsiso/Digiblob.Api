using Digiblob.Api.Auth.Handlers.Commands.Interfaces;
using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Handlers.Commands;

public sealed class CreateUserHandler : ICommandHandler<CreateUserCommand, Unit>
{
    private readonly DataContext _dataContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILookupNormalizer _lookupNormalizer;
    private readonly ISecurityStampProvider _securityStampProvider;

    public CreateUserHandler(
        DataContext dataContext,
        IPasswordHasher passwordHasher,
        ILookupNormalizer lookupNormalizer,
        ISecurityStampProvider securityStampProvider)
    {
        _dataContext = dataContext;
        _passwordHasher = passwordHasher;
        _lookupNormalizer = lookupNormalizer;
        _securityStampProvider = securityStampProvider;
    }
    
    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Map validated properties
        var userToCreate = request.Adapt<User>();
        
        // Set normalized email and user name
        userToCreate.NormalizedEmail = _lookupNormalizer.Normalize(request.Email);
        userToCreate.NormalizedUserName = _lookupNormalizer.Normalize(request.UserName);

        // Set required properties
        userToCreate.SecurityStamp = _securityStampProvider.GetStamp();
        userToCreate.PasswordHash = _passwordHasher.HashPassword(request.Password);
        
        // Create a user in the persistence store
        await _dataContext.Users.AddAsync(userToCreate, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);
        
        // Return
        return new Unit();
    }
}