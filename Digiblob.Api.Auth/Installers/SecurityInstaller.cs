using Digiblob.Api.Auth.Installers.Interfaces;
using Digiblob.Api.Auth.Services;
using Digiblob.Api.Auth.Services.Interfaces;

namespace Digiblob.Api.Auth.Installers;

public sealed class SecurityInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
    }
}