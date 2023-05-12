using Digiblob.Api.Auth.Installers.Interfaces;

namespace Digiblob.Api.Auth.Installers;

public sealed class DbInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddSqlServer<DataContext>(
            "Server=(.);Database=Digiblob;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;",
            option => option.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));

    }
}