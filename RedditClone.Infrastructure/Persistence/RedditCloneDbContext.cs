namespace RedditClone.Infrastructure.Persistence;

using MediatR;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserCommunitiesAggregate;

public class RedditCloneDbContext : DbContext
{
    private readonly IPublisher _publisher;
    public RedditCloneDbContext(
        DbContextOptions<RedditCloneDbContext> options,
        IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Community> Communities { get; set; } = null!;
    public DbSet<UserCommunities> UserCommunities { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(
                typeof(RedditCloneDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .SelectMany(e => e.DomainEvents);

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}