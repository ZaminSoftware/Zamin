using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Domain.Blogs.Entities;
using Zamin.Infra.Data.Sqlite.Commands;

namespace MiniBlog.Infra.Data.Sqlite.Commands.Common;

public class MiniblogCommandDbContext(DbContextOptions<MiniblogCommandDbContext> options) : BaseCommandDbContext(options)
{
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}