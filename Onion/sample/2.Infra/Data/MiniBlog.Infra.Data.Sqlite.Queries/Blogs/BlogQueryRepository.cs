using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Contracts.Blogs.Queries;
using MiniBlog.Core.RequestResponse.Blogs.Queries.GetById;
using MiniBlog.Infra.Data.Sqlite.Queries.Common;
using Zamin.Infra.Data.Sqlite.Queries;

namespace MiniBlog.Infra.Data.Sqlite.Queries.Blogs;

public class BlogQueryRepository(MiniblogQueryDbContext dbContext) : BaseQueryRepository<MiniblogQueryDbContext>(dbContext), IBlogQueryRepository
{
    public async Task<BlogQr?> ExecuteAsync(GetBlogByIdQuery query)
        => await _dbContext.Blogs.Select(c => new BlogQr()
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description
        }).FirstOrDefaultAsync(c => c.Id.Equals(query.BlogId));
}