using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Validators;

/// <summary>
///     Validator of the user model to create.
/// </summary>
public sealed class SigninCommandValidator : AbstractValidator<SigninCommand>
{
    private readonly DataContext _dataContext;
    private readonly ILookupNormalizer _lookupNormalizer;

    /// <summary>
    ///     Creates a set of rules to validate the user model to create.
    /// </summary>
    public SigninCommandValidator(ILookupNormalizer lookupNormalizer, DataContext dataContext)
    {
        this._lookupNormalizer = lookupNormalizer;
        this._dataContext = dataContext;

        this.RuleLevelCascadeMode = CascadeMode.Stop;

        this.RuleFor(user => user.Email).NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email format is not valid.")
            .MaximumLength(Constraints.MaximumEmailLength)
            .WithMessage("Email is too long.")
            .Must(this.EmailNotTaken)
            .WithMessage("Email already taken.");

        this.RuleFor(user => user.UserName).NotEmpty()
            .WithMessage("User name is required.")
            .MinimumLength(Constraints.MinimumUserNameLength)
            .WithMessage("User name is too short.")
            .MaximumLength(Constraints.MaximumUserNameLength)
            .WithMessage("User name is too long.")
            .Must(UserNameContainsValidCharacters)
            .WithMessage("User name contains invalid characters.")
            .Must(this.UserNameNotTaken)
            .WithMessage("User name already taken.");

        this.RuleFor(user => user.GivenName).NotEmpty()
            .WithMessage("Given name is required.")
            .MinimumLength(Constraints.MinimumGivenNameLength)
            .WithMessage("Given name is too short.")
            .MaximumLength(Constraints.MaximumGivenNameLength)
            .WithMessage("Given name is too long.");

        this.RuleFor(user => user.FamilyName).NotEmpty()
            .WithMessage("Family name is required.")
            .MinimumLength(Constraints.MinimumFamilyNameLength)
            .WithMessage("Family name is too short.")
            .MaximumLength(Constraints.MaximumFamilyNameLength)
            .WithMessage("Family name is too long.");

        this.RuleFor(user => user.Password).NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(Constraints.MinimumPasswordLength)
            .WithMessage("Password is too short.")
            .MaximumLength(Constraints.MaximumPasswordLength)
            .WithMessage("Password is too long.")
            .Must(PasswordContainsValidCharacters)
            .WithMessage("Password contains invalid characters.");
    }

    private static bool UserNameContainsValidCharacters(string userName)
    {
        return userName.All(c => Constraints.AllowedUserNameCharacters.IndexOf(c, StringComparison.Ordinal) != -1);
    }

    private static bool PasswordContainsValidCharacters(string password)
    {
        return password.All(c => Constraints.AllowedPasswordCharacters.IndexOf(c, StringComparison.Ordinal) != -1);
    }

    private bool EmailNotTaken(string email)
    {
        var normalizedEmail = this._lookupNormalizer.Normalize(email);
        return !this._dataContext.Users.Any(u => u.NormalizedEmail == normalizedEmail);
    }

    private bool UserNameNotTaken(string userName)
    {
        var normalizedUserName = this._lookupNormalizer.Normalize(userName);
        return !this._dataContext.Users.Any(u => u.NormalizedUserName == normalizedUserName);
    }
}