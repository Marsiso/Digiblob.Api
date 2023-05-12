using Digiblob.Api.Auth.Installers.Interfaces;
using Digiblob.Api.Auth.Validators;

namespace Digiblob.Api.Auth.Installers;

public sealed class ValidatorInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
        services.AddScoped<IValidator<LoginQuery>, LoginQueryValidator>();
    }
}