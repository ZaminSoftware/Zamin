using MiniBlog.Core.Contracts.Blogs.Commands;
using MiniBlog.Core.Domain.Blogs.Entities;
using MiniBlog.Infra.Data.Sqlite.Commands.Common;
using Zamin.Infra.Data.Sqlite.Commands;

namespace MiniBlog.Infra.Data.Sqlite.Commands.Blogs;

public class BlogCommandRepository(MiniblogCommandDbContext dbContext) : BaseCommandRepository<Blog, MiniblogCommandDbContext, int>(dbContext), IBlogCommandRepository
{
}