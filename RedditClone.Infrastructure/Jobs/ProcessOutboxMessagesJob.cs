namespace RedditClone.Infrastructure.Jobs;

using Quartz;
using MediatR;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.Primitives;
using RedditClone.Infrastructure.Outbox;
using RedditClone.Infrastructure.Persistence;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob(
    RedditCloneDbContext dbContext,
    IPublisher publisher) : IJob
{
    private readonly RedditCloneDbContext _dbContext = dbContext;
    private readonly IPublisher _publisher = publisher;

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainEvent is null)
            {
                continue;
            }

            await _publisher.Publish(domainEvent, context.CancellationToken);

            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}