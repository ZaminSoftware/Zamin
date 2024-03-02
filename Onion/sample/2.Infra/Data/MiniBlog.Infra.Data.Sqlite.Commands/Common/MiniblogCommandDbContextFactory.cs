using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MiniBlog.Infra.Data.Sqlite.Commands.Common;

public sealed class MiniblogCommandDbContextFactory : IDesignTimeDbContextFactory<MiniblogCommandDbContext>
{
    public MiniblogCommandDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .Build();

        var builder = new DbContextOptionsBuilder<MiniblogCommandDbContext>();

        builder.UseSqlite(configuration.GetConnectionString("CommandDb_ConnectionString"));

        return new MiniblogCommandDbContext(builder.Options);
    }
}