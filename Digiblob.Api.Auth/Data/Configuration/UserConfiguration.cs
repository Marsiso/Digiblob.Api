using Digiblob.Api.Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Digiblob.Api.Auth.Data.Configuration;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(u => u.Id)
            .IsClustered();

        builder.HasIndex(u => new { u.NormalizedUserName, u.NormalizedEmail })
            .IsUnique()
            .IsClustered();

        #region Properties

        builder.Property(u => u.Id)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName("id");

        builder.Property(u => u.UserName)
            .IsRequired()
            .IsUnicode()
            .HasColumnName("user_name")
            .HasMaxLength(256);

        builder.Property(u => u.NormalizedUserName)
            .IsRequired()
            .IsUnicode()
            .HasColumnName("normalized_user_name")
            .HasMaxLength(256);

        builder.Property(u => u.Email)
            .IsRequired()
            .IsUnicode()
            .HasColumnName("email")
            .HasMaxLength(256);

        builder.Property(u => u.NormalizedEmail)
            .IsRequired()
            .IsUnicode()
            .HasColumnName("normalized_email")
            .HasMaxLength(256);

        builder.Property(u => u.GivenName)
            .IsRequired()
            .IsUnicode()
            .HasColumnName("given_name")
            .HasMaxLength(256);

        builder.Property(u => u.FamilyName)
            .IsRequired()
            .IsUnicode()
            .HasColumnName("family_name")
            .HasMaxLength(256);

        builder.Property(u => u.ConcurrencyStamp)
            .IsRequired()
            .IsConcurrencyToken()
            .IsRowVersion()
            .HasColumnName("concurrency_stamp");

        builder.Property(u => u.SecurityStamp)
            .IsRequired()
            .IsUnicode()
            .HasColumnName("security_stamp");

        builder.Property(u => u.AccessFailedCount)
            .IsRequired()
            .HasColumnName("access_failed_count");

        builder.Property(u => u.DateLockoutEnd)
            .HasColumnName("date_lockout_end");

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