using Digiblob.Api.Auth.Queries;
using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Validators;

public sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    private readonly ILookupNormalizer _lookupNormalizer;
    private readonly DataContext _dataContext;
    
    public LoginQueryValidator(ILookupNormalizer lookupNormalizer, DataContext dataContext)
    {
        _lookupNormalizer = lookupNormalizer;
        _dataContext = dataContext;
        
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(user => user.UserName).NotEmpty()
            .WithMessage("User name is required")
            .Must(UserExists)
            .WithMessage("Invalid user name or password");

        RuleFor(user => user.Password).NotEmpty()
            .WithMessage("Password is required");
    }
    
    private bool UserExists(string userName)
    {
        var normalizedUserName = _lookupNormalizer.Normalize(userName);
        return _dataContext.Set<User>().Any(u => u.NormalizedEmail == normalizedUserName || u.NormalizedUserName == normalizedUserName);
    }
}