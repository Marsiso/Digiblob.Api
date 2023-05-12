using Digiblob.Api.Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Digiblob.Api.Auth.Data.Configuration;

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_roles");

        builder.HasKey(u => new { u.UserId, u.RoleId })
            .IsClustered();
    }
}