namespace RedditClone.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserCommunitiesAggregate;

public class RedditCloneDbContext : DbContext
{
    public RedditCloneDbContext(DbContextOptions<RedditCloneDbContext> options)
        : base(options) { }

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

}