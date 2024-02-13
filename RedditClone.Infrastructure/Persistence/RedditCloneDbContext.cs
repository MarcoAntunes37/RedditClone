namespace RedditClone.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.UserAggregate;

public class RedditCloneDbContext : DbContext
{
    public RedditCloneDbContext(DbContextOptions<RedditCloneDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Community> Communities { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

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
        modelBuilder.Entity<User>()
            .HasMany<Community>()
            .WithOne();

        modelBuilder.Entity<User>()
            .HasMany<Community>()
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
            "UserCommunities",
                j => j
                    .HasOne<Community>()
                    .WithMany()
                    .HasForeignKey("CommunityId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasKey("CommunityId", "UserId"));


    }
}