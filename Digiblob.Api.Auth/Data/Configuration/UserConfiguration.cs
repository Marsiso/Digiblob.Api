using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Digiblob.Api.Auth.Data.Configuration;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(Tables.Users, Schema);

        builder.HasKey(u => u.Id)
            .IsClustered();

        builder.HasIndex(u => new { u.NormalizedUserName, u.NormalizedEmail })
            .IsUnique();

        #region Properties

        builder.Property(u => u.Id)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName(Columns.PrimaryKeyColumnName);

        builder.Property(u => u.UserName)
            .IsRequired()
            .IsUnicode()
            .HasColumnName(Columns.UserNameColumnName)
            .HasMaxLength(Constraints.MaximumUserNameLength);

        builder.Property(u => u.NormalizedUserName)
            .IsRequired()
            .IsUnicode()
            .HasColumnName(Columns.NormalizedUserNameColumnName)
            .HasMaxLength(Constraints.MaximumUserNameLength);

        builder.Property(u => u.Email)
            .IsRequired()
            .IsUnicode()
            .HasColumnName(Columns.EmailColumnName)
            .HasMaxLength(Constraints.MaximumEmailLength);

        builder.Property(u => u.NormalizedEmail)
            .IsRequired()
            .IsUnicode()
            .HasColumnName(Columns.NormalizedEmailColumnName)
            .HasMaxLength(Constraints.MaximumEmailLength);

        builder.Property(u => u.EmailConfirmed)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName(Columns.EmailConfirmedColumnName);

        builder.Property(u => u.GivenName)
            .IsRequired()
            .IsUnicode()
            .HasColumnName(Columns.GivenNameColumnName)
            .HasMaxLength(Constraints.MaximumGivenNameLength);

        builder.Property(u => u.FamilyName)
            .IsRequired()
            .IsUnicode()
            .HasColumnName(Columns.FamilyNameColumnName)
            .HasMaxLength(Constraints.MaximumFamilyNameLength);

        builder.Property(u => u.ConcurrencyStamp)
            .IsRequired()
            .IsConcurrencyToken()
            .IsRowVersion()
            .HasColumnName(Columns.ConcurrencyStampColumnName);

        builder.Property(u => u.SecurityStamp)
            .IsRequired()
            .IsUnicode()
            .HasColumnName(Columns.SecurityStampColumnName)
            .HasMaxLength(Constraints.SecurityStampLength);

        builder.Property(u => u.AccessFailedCount)
            .IsRequired()
            .HasColumnName(Columns.AccessFailCountColumnName);

        builder.Property(u => u.DateLockoutEnd)
            .HasColumnName(Columns.DateLockoutEndColumnName);

        #endregion

        #region Relations

        builder.HasMany(u => u.Roles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}