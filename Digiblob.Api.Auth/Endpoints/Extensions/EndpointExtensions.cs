namespace Digiblob.Api.Auth.Endpoints.Extensions;

public static class EndpointExtensions
{
    public static void AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
    {
        var endpointDefinitions = new List<IEndpointDefinition>();

        foreach (var scanMarker in scanMarkers)
        {
            endpointDefinitions.AddRange(scanMarker.Assembly.ExportedTypes.Where(exportedType =>
                    typeof(IEndpointDefinition).IsAssignableFrom(exportedType) &&
                    exportedType is { IsAbstract: false, IsInterface: false })
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>());
        }

        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.DefineServices(services);
        }

        services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
    }

    public static void UseEndpointDefinitions(this WebApplication application)
    {
        var endpointDefinitions = application.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();
        foreach (var endpointDefinition in endpointDefinitions)
        {
            endpointDefinition.DefineEndpoints(application);
        }
    }
}