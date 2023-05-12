using Digiblob.Api.Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Digiblob.Api.Auth.Data.Configuration;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(r => r.Id)
            .IsClustered();

        builder.HasIndex(r => r.NormalizedName)
            .IsUnique()
            .IsClustered();

        #region Properties

        builder.Property(r => r.Id)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName("id");
        
        builder.Property(u => u.Name)
            .IsRequired()
            .IsUnicode()
            .HasColumnName("name")
            .HasMaxLength(256);

        builder.Property(u => u.NormalizedName)
            .IsRequired()
            .IsUnicode()
            .HasColumnName("normalized_name")
            .HasMaxLength(256);

        builder.Property(u => u.ConcurrencyStamp)
            .IsConcurrencyToken()
            .IsRowVersion()
            .IsRequired()
            .HasColumnName("concurrency_stamp");

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