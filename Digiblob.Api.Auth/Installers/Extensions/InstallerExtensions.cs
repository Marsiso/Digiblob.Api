﻿using Digiblob.Api.Auth.Installers.Interfaces;

namespace Digiblob.Api.Auth.Installers.Extensions;

public static class InstallerExtensions
{
    public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var installers = typeof(Program).Assembly.ExportedTypes
            .Where(exportedType =>
                typeof(IInstaller).IsAssignableFrom(exportedType) &&
                exportedType is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>()
            .ToList();

        installers.ForEach(installer => installer.InstallServices(services, configuration, environment));
    }
}