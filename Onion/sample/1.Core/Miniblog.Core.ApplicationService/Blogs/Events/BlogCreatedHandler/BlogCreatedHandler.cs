using Microsoft.Extensions.Logging;
using MiniBlog.Core.Domain.Blogs.Events;
using Zamin.Core.Contracts.ApplicationServices.Events;

namespace MiniBlog.Core.ApplicationService.Blogs.Events.BlogCreatedHandler;

public class BlogCreatedHandler(ILogger<BlogCreatedHandler> logger) : IDomainEventHandler<BlogCreated>
{
    private readonly ILogger<BlogCreatedHandler> _logger = logger;

    public async Task Handle(BlogCreated Event)
    {
        await Task.Delay(5);
    }
}
