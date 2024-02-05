using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.UserAggregate;

namespace RedditClone.Infrastructure.Persistence;

public class RedditCloneDbContext : DbContext
{
    public RedditCloneDbContext(DbContextOptions<RedditCloneDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserAggregate> Users { get; set; } = null!;
    public DbSet<CommunityAggregate> Communities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(
                typeof(RedditCloneDbContext).Assembly);

        modelBuilder.Entity<UserAggregate>()
            .HasMany<CommunityAggregate>()
            .WithOne()
            .HasForeignKey("OwnerId")
            .IsRequired();

        modelBuilder.Entity<UserAggregate>()
            .HasMany<CommunityAggregate>()
            .WithMany()
            .UsingEntity("UserCommunities");

        base.OnModelCreating(modelBuilder);
    }
}