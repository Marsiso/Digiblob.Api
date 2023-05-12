using Digiblob.Api.Auth.Installers.Interfaces;

namespace Digiblob.Api.Auth.Installers;

public sealed class DbInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddDbContext<DataContext>();
    }
}