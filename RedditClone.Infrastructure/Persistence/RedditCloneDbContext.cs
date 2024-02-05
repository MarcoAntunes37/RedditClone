using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.UserAggregate;

namespace RedditClone.Infrastructure.Persistence;

public class RedditCloneDbContext : DbContext
{
    public RedditCloneDbContext(DbContextOptions<RedditCloneDbContext> options)
        : base(options) { }

    public DbSet<UserAggregate> Users { get; set; } = null!;
    public DbSet<CommunityAggregate> Communities { get; set; } = null!;
    public DbSet<PostAggregate> Posts { get; set; } = null!;
    public DbSet<CommentAggregate> Comments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(
                typeof(RedditCloneDbContext).Assembly);

        ConfigureRelations(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    protected void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAggregate>()
            .HasMany<CommunityAggregate>()
            .WithOne()
            .HasForeignKey("OwnerId")
            .IsRequired();

        modelBuilder.Entity<UserAggregate>()
            .HasMany<CommunityAggregate>()
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
            "UserCommunities",
            j => j
                .HasOne<CommunityAggregate>()
                .WithMany()
                .HasForeignKey("CommunityId")
                .OnDelete(DeleteBehavior.Cascade),
            j => j
                .HasOne<UserAggregate>()
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade),
            j => j
                .HasKey("CommunityId", "UserId"));

        modelBuilder.Entity<UserAggregate>()
            .HasMany<PostAggregate>()
            .WithOne()
            .HasForeignKey("UserId")
            .IsRequired();
    }
}