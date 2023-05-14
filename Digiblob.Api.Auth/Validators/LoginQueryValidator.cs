using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Validators;

public sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    private readonly DataContext _dataContext;
    private readonly ILookupNormalizer _lookupNormalizer;

    public LoginQueryValidator(ILookupNormalizer lookupNormalizer, DataContext dataContext)
    {
        this._lookupNormalizer = lookupNormalizer;
        this._dataContext = dataContext;

        this.RuleLevelCascadeMode = CascadeMode.Stop;

        this.RuleFor(user => user.UserName).NotEmpty()
            .WithMessage("User name is required")
            .Must(this.UserExists)
            .WithMessage("Invalid user name or password");

        this.RuleFor(user => user.Password).NotEmpty()
            .WithMessage("Password is required");
    }

    private bool UserExists(string userName)
    {
        var normalizedUserName = this._lookupNormalizer.Normalize(userName);
        return this._dataContext.Set<User>().Any(u =>
            u.NormalizedEmail == normalizedUserName || u.NormalizedUserName == normalizedUserName);
    }
}