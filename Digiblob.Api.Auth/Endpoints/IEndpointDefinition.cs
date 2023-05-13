namespace Digiblob.Api.Auth.Endpoints;

public interface IEndpointDefinition
{
    void DefineEndpoints(WebApplication application);
    void DefineServices(IServiceCollection services);
}