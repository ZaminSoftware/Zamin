using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamin.Utilities.SoftwareParts.Detection.Detectors;
using Zamin.Utilities.SoftwareParts.Detection.Options;
using Zamin.Utilities.SoftwareParts.Detection.Services;

namespace Zamin.Extensions.DependencyInjection;

public static class SoftwarePartDetectionServiceCollectionExtensions
{
    public static IServiceCollection AddZaminSoftwareDetectionService(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        return services.AddZaminSoftwareDetectionService(configuration.GetSection(sectionName));
    }

    public static IServiceCollection AddZaminSoftwareDetectionService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SoftwarePartDetectionOption>(configuration);

        var option = configuration.Get<SoftwarePartDetectionOption>() ?? new();

        return services.AddServices(option);
    }

    public static IServiceCollection AddZaminSoftwareDetectionService(this IServiceCollection services, Action<SoftwarePartDetectionOption> setupAction)
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
        services.AddTransient<SoftwarePartAuthenticatorService>();
        services.AddTransient<SoftwarePartPublisherService>();

        var authenticatorHttpClient = services.AddHttpClient<SoftwarePartAuthenticatorService>();
        if (option.Authentication.IgnoreSSL)
        {
            authenticatorHttpClient.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            });
        }

        var publisherHttpClient = services.AddHttpClient<SoftwarePartPublisherService>();
        if (option.Registration.IgnoreSSL)
        {
            publisherHttpClient.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            });
        }

        return services;
    }

    public static async Task UseZaminSoftwarePartDetection(this WebApplication app)
    {
        var publisher = app.Services.GetRequiredService<SoftwarePartPublisherService>();

        await publisher.ExecuteAsync();
    }
}