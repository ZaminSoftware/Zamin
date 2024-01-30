using Zamin.Services;
using Zamin.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddZaminSoftwarePartDetector(action =>
{
    action.ApplicationName = "Application";
    action.ModuleName = "Module";
    action.ServiceName = "Service";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var part = app.Services.GetRequiredService<SoftwarePartDetectorService>().Execute();

app.Run();