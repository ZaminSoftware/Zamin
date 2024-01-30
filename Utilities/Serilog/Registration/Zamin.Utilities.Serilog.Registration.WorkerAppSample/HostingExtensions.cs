using Microsoft.AspNetCore.Builder;

namespace Zamin.Utilities.Serilog.Registration.WorkerAppSample;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHostedService<Worker>();

        return builder.Build();
    }
}