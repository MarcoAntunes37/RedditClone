namespace RedditClone.Infrastructure.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class PostConfiguration
 : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        ConfigurePostTable(builder);
        ConfigurePostVotesTable(builder);
    }

    private void ConfigurePostVotesTable(EntityTypeBuilder<Post> builder)
    {
        builder.OwnsMany(p => p.Votes, pvb =>
        {
            pvb.ToTable("PostsVotes");

            pvb.WithOwner()
                .HasForeignKey();

            pvb.HasKey("Id");

            pvb.Property(pv => pv.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => new VoteId(value));

            pvb.Property(pv => pv.UserId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => new UserId(value));

            pvb.HasOne<User>()
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            pvb.Property(pv => pv.PostId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => new PostId(value));

            pvb.Property(pv => pv.IsVoted);

            pvb.HasIndex(pv => new {pv.UserId, pv.PostId})
                .IsUnique();
        });
    }

    private void ConfigurePostTable(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => new PostId(value));

        builder.Property(p => p.UserId)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => new UserId(value));

        builder.HasOne<User>()
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.CommunityId)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => new CommunityId(value));

        builder.HasOne<Community>()
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Title)
            .HasMaxLength(100);

        builder.Property(p => p.Content);

        builder.Property(p => p.CreatedAt);

        builder.Property(p => p.UpdatedAt);
    }
}