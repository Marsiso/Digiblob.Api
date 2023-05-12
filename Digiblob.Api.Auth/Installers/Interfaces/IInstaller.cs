namespace Digiblob.Api.Auth.Installers.Interfaces;

public interface IInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment);
}