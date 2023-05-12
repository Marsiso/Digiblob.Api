namespace Digiblob.Api.Auth.Installers;

public interface IInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment);
}