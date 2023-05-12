namespace Digiblob.Api.Auth.Middlewares.Extensions;

public static class SwaggerExtensions
{
    public static void ConfigureSwagger(this IApplicationBuilder applicationBuilder, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            applicationBuilder.UseSwagger(option =>
            {
                option.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            applicationBuilder.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("v1/swagger.json", "v1");
            });
        }
    }
}