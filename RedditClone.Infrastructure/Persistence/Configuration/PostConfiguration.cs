namespace RedditClone.Infrastructure.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.ValueObjects;

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
                    value => VoteId.Create(value));

            pvb.Property(pv => pv.UserId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => UserId.Create(value));

            pvb.Property(pv => pv.PostId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => PostId.Create(value));

            pvb.Property(pv => pv.IsVoted);
        });
    }

    private void ConfigurePostTable(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => PostId.Create(value));

        builder.Property(p => p.UserId)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => UserId.Create(value));

        builder.Property(p => p.CommunityId)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => CommunityId.Create(value));

        builder.Property(p => p.Title)
            .HasMaxLength(100);

        builder.Property(p => p.Content);

        builder.Property(p => p.CreatedAt);

        builder.Property(p => p.UpdatedAt);
    }
}