using Digiblob.Api.Auth.Installers.Interfaces;

namespace Digiblob.Api.Auth.Installers;

public sealed class DbInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddSqlServer<DataContext>(
            DbDefaults.ConnectionString,
            option => option.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));

    }
}