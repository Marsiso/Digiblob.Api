namespace Digiblob.Api.Auth.Data.Context;

public sealed class DataContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;
    public DbSet<UserRole> UserRoles { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString, option =>
        {
            option.MinBatchSize(1);
            option.MaxBatchSize(100);
            option.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
    }
}