namespace Digiblob.Api.Auth.Data.Entities;

public sealed class User : Entity
{
    public string GivenName { get; set; } = default!;
    public string FamilyName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string NormalizedUserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string NormalizedEmail { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public byte[] ConcurrencyStamp { get; set; } = default!;
    public string SecurityStamp { get; set; } = default!;
    public int AccessFailedCount { get; set; }
    public DateTimeOffset? DateLockoutEnd { get; set; }
    public ICollection<UserRole> Roles { get; set; } = default!;
}