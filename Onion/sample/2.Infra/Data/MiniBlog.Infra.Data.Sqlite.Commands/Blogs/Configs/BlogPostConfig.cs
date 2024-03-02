using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniBlog.Core.Domain.Blogs.Entities;

namespace MiniBlog.Infra.Data.Sqlite.Commands.Blogs.Configs;

public sealed class BlogPostConfig : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {

    }
}
