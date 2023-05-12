using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Digiblob.Api.Auth.Data.Configuration;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(Tables.Roles, DbDefaults.Schema);

        builder.HasKey(r => r.Id)
            .IsClustered();

        builder.HasIndex(r => r.NormalizedName)
            .IsUnique();

        #region Properties

        builder.Property(r => r.Id)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName(Columns.PrimaryKeyColumnName);
        
        builder.Property(u => u.Name)
            .IsRequired()
            .IsUnicode()
            .HasColumnName(Columns.RoleNameColumnName)
            .HasMaxLength(Constraints.MaximumRoleNameLength);

        builder.Property(u => u.NormalizedName)
            .IsRequired()
            .IsUnicode()
            .HasColumnName(Columns.NormalizedRoleNameColumnName)
            .HasMaxLength(Constraints.MaximumRoleNameLength);

        builder.Property(u => u.ConcurrencyStamp)
            .IsConcurrencyToken()
            .IsRowVersion()
            .IsRequired()
            .HasColumnName(Columns.ConcurrencyStampColumnName);

        #endregion

        #region Relations

        builder.HasMany(r => r.Users)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}