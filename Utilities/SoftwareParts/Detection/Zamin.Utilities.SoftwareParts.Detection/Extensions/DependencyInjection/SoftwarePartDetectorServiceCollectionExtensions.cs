using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Services;
using Zamin.Utilities.SoftwareParts.Detection.Detectors;
using Zamin.Utilities.SoftwareParts.Detection.Options;

namespace Zamin.Extensions.DependencyInjection;

public static class SoftwarePartDetectorServiceCollectionExtensions
{
    public static IServiceCollection AddZaminSoftwarePartDetector(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        return services.AddZaminSoftwarePartDetector(configuration.GetSection(sectionName));
    }

    public static IServiceCollection AddZaminSoftwarePartDetector(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SoftwarePartDetectionOption>(configuration);

        var option = configuration.Get<SoftwarePartDetectionOption>() ?? new();

        return services.AddServices(option);
    }

    public static IServiceCollection AddZaminSoftwarePartDetector(this IServiceCollection services, Action<SoftwarePartDetectionOption> setupAction)
    {
        var option = new SoftwarePartDetectionOption();

        setupAction.Invoke(option);

        return services.AddServices(option).Configure(setupAction);
    }

    private static IServiceCollection AddServices(this IServiceCollection services, SoftwarePartDetectionOption option)
    {
        services.AddTransient<ControllersAndActionDetector>();
        services.AddTransient<SoftwarePartDetector>();
        services.AddTransient<SoftwarePartDetectorService>();

        return services;
    }
}