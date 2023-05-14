using System.Reflection;
using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Data.Services;

public sealed class RoleSeed : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public RoleSeed(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Required services
        await using var scope = this._serviceProvider.CreateAsyncScope();
        await using var dataContext = scope.ServiceProvider.GetService<DataContext>();
        var normalizer = scope.ServiceProvider.GetService<ILookupNormalizer>();

        // Null check
        ArgumentNullException.ThrowIfNull(dataContext);
        ArgumentNullException.ThrowIfNull(normalizer);

        // Seed roles
        var roles = GetRoles(normalizer);
        foreach (var role in roles)
        {
            if (await dataContext.Roles.AnyAsync(r => r.NormalizedName == role.NormalizedName, cancellationToken))
            {
                continue;
            }

            await dataContext.Roles.AddAsync(role, cancellationToken);
            Log.Information("The role with name {Role} has been scheduled for creation in the database", role.Name);
        }

        await dataContext.SaveChangesAsync(cancellationToken);
        if (dataContext.ChangeTracker.Entries().Any())
        {
            Log.Information("The scheduled roles to be created have been created in the database");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }

    private static List<Role> GetRoles(ILookupNormalizer normalizer)
    {
        return typeof(Roles).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => fieldInfo is { IsLiteral: true, IsInitOnly: false })
            .Select(fieldInfo =>
            {
                var value = (string?)fieldInfo.GetRawConstantValue();
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidOperationException();
                }

                return new Role { Name = value, NormalizedName = normalizer.Normalize(value) };
            })
            .ToList();
    }
}