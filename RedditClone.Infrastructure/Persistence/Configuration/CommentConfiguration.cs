using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.ValueObjects;

namespace RedditClone.Infrastructure.Persistence.Configuration;

public class CommentConfiguration
 : IEntityTypeConfiguration<CommentAggregate>
{
    public void Configure(EntityTypeBuilder<CommentAggregate> builder)
    {
        ConfigureCommentsTable(builder);
    }

    private void ConfigureCommentsTable(EntityTypeBuilder<CommentAggregate> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
             .ValueGeneratedNever()
             .HasColumnName("CommentId")
             .HasConversion(id => id.Value,
                 value => CommentId.Create(value));

        builder.Property(c => c.Content)
            .HasMaxLength(255);

        builder.Property(c => c.CreatedAt);

        builder.Property(c => c.UpdatedAt);
    }
}