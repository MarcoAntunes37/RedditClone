namespace RedditClone.Infrastructure.Persistence;

using Newtonsoft.Json;
using RedditClone.Domain.Primitives;
using RedditClone.Infrastructure.Outbox;
using RedditClone.Application.Common.Interfaces.Persistence;


public class UnitOfWork(RedditCloneDbContext dbContext) : IUnitOfWork
{
    private readonly RedditCloneDbContext _dbContext = dbContext;

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDomainEventsToOutboxMessages();

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void ConvertDomainEventsToOutboxMessages()
    {
        var outboxMessages = _dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.GetDomainEvents();
                aggregateRoot.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    }
                )
            })
            .ToList();

        _dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}