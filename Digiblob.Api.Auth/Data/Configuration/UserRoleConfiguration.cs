using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Digiblob.Api.Auth.Data.Configuration;

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(Tables.UserRoles, Schema);

        builder.HasKey(u => new { u.UserId, u.RoleId })
            .IsClustered();

        #region Properties

        builder.Property(ur => ur.UserId)
            .IsRequired()
            .HasColumnName(Columns.UserForeignKeyColumnName);
        
        builder.Property(ur => ur.RoleId)
            .IsRequired()
            .HasColumnName(Columns.RoleForeignKeyColumnName);

        #endregion
    }
}