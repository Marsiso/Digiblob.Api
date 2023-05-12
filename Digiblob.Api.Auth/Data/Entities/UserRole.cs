namespace Digiblob.Api.Auth.Data.Entities;

public sealed class UserRole
{
    public long UserId { get; set; }
    public long RoleId { get; set; }
    public User User { get; set; } = default!;
    public Role Role { get; set; } = default!;
}