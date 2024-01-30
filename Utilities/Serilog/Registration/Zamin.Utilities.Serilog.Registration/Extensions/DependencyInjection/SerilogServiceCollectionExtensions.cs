using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;
using Zamin.Utilities.Serilog.Registration.Enrichers;
using Zamin.Utilities.Serilog.Registration.Options;

namespace Zamin.Extensions.DependencyInjection;

public static class SerilogServiceCollectionExtensions
{
    public static WebApplicationBuilder AddZaminSerilog(this WebApplicationBuilder builder, IConfiguration configuration, string sectionName, params Type[] enrichersType)
    {
        return builder.AddZaminSerilog(configuration.GetSection(sectionName), enrichersType);
    }
    public static WebApplicationBuilder AddZaminSerilog(this WebApplicationBuilder builder, IConfiguration configuration, params Type[] enrichersType)
    {
        builder.Services.Configure<SerilogApplicationEnricherOptions>(configuration);
        return AddServices(builder, enrichersType);
    }

    public static WebApplicationBuilder AddZaminSerilog(this WebApplicationBuilder builder, Action<SerilogApplicationEnricherOptions> setupAction, params Type[] enrichersType)
    {
        builder.Services.Configure(setupAction);
        return AddServices(builder, enrichersType);
    }

    private static WebApplicationBuilder AddServices(WebApplicationBuilder builder, params Type[] enrichersType)
    {
        builder.Services.AddTransient<ZaminUserInfoEnricher>();
        builder.Services.AddTransient<ZaminApplicaitonEnricher>();

        foreach (var enricherType in enrichersType)
        {
            builder.Services.AddTransient(enricherType);
        }

        builder.Host.UseSerilog((ctx, services, lc) =>
        {
            List<ILogEventEnricher> logEventEnrichers = [];
            logEventEnrichers.Add(services.GetRequiredService<ZaminUserInfoEnricher>());
            logEventEnrichers.Add(services.GetRequiredService<ZaminApplicaitonEnricher>());
            foreach (var enricherType in enrichersType)
            {
                logEventEnrichers.Add(services.GetRequiredService(enricherType) as ILogEventEnricher);
            }

            lc.Enrich.FromLogContext()
              .Enrich.With([.. logEventEnrichers])
              .Enrich.WithExceptionDetails()
              .Enrich.WithSpan()
              .ReadFrom.Configuration(ctx.Configuration);
        });

        return builder;
    }
}