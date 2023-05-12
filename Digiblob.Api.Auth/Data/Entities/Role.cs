namespace Digiblob.Api.Auth.Data.Entities;

public sealed class Role : Entity
{
    public string Name { get; set; } = default!;
    public string NormalizedName { get; set; } = default!;
    public byte[] ConcurrencyStamp { get; set; } = default!;
    public ICollection<UserRole> Users { get; set; } = default!;
}