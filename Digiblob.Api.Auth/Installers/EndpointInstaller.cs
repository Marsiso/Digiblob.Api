using Digiblob.Api.Auth.Endpoints.Extensions;
using Digiblob.Api.Auth.Installers.Interfaces;

namespace Digiblob.Api.Auth.Installers;

public sealed class EndpointInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddEndpointDefinitions(typeof(Program));
    }
}