using Digiblob.Api.Auth.Installers.Interfaces;
using Digiblob.Api.Auth.Validations;

namespace Digiblob.Api.Auth.Installers;

public sealed class MediatRInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddMediatR(mediatRServiceConfiguration =>
        {
            mediatRServiceConfiguration.RegisterServicesFromAssembly(typeof(Program).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
    }
}