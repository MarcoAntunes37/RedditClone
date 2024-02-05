using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.ValueObjects;

namespace RedditClone.Infrastructure.Persistence.Configuration;

public class PostConfiguration
 : IEntityTypeConfiguration<PostAggregate>
{
    public void Configure(EntityTypeBuilder<PostAggregate> builder)
    {
        ConfigurePostTable(builder);
    }

    private void ConfigurePostTable(EntityTypeBuilder<PostAggregate> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
             .ValueGeneratedNever()
             .HasColumnName("PostId")
             .HasConversion(id => id.Value,
                 value => PostId.Create(value));


        builder.Property(c => c.CreatedAt);

        builder.Property(c => c.UpdatedAt);
    }
}