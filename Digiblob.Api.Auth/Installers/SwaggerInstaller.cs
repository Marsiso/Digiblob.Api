using Digiblob.Api.Auth.Installers.Interfaces;
using Microsoft.OpenApi.Models;

namespace Digiblob.Api.Auth.Installers;

public sealed class SwaggerInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Digiblob Auth API Docs",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Email = "olsak.marek@outlook.cz",
                    Name = "Marek Olšák",
                    Url = new Uri("https://www.linkedin.com/in/marek-ol%C5%A1%C3%A1k-1715b724a/")
                },
                Description = "OpenAPI specification for the test only purposes and the API documentation."
            });
        });
    }
}