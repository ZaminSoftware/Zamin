using Zamin.Core.Contracts.Data.Queries;

namespace Zamin.Infra.Data.Sqlite.Queries;

public class BaseQueryRepository<TDbContext>(TDbContext dbContext) : IQueryRepository where TDbContext : BaseQueryDbContext
{
    protected readonly TDbContext _dbContext = dbContext;
}