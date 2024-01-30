using Zamin.Extensions;
using Zamin.Extensions.DependencyInjection;
using Zamin.Utilities.Serilog.Registration.WebAppSample;

SerilogExtensions.RunWithSerilogExceptionHandling(() =>
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddZaminSerilog(c =>
    {
        c.ApplicationName = "SerilogRegistration";
        c.ServiceName = "SampleService";
        c.ServiceVersion = "1.0";
        c.ServiceId = Guid.NewGuid().ToString();
    });

    var app = builder.ConfigureServices().ConfigurePipeline();

    app.Run();
});