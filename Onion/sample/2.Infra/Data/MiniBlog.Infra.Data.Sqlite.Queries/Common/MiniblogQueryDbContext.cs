using Microsoft.EntityFrameworkCore;
using MiniBlog.Infra.Data.Sqlite.Queries.Blogs;
using Zamin.Infra.Data.Sqlite.Queries;

namespace MiniBlog.Infra.Data.Sqlite.Queries.Common;

public class MiniblogQueryDbContext(DbContextOptions<MiniblogQueryDbContext> options) : BaseQueryDbContext(options)
{
    public virtual DbSet<Blog> Blogs { get; set; } = null!;
    public virtual DbSet<OutBoxEventItem> OutBoxEventItems { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
