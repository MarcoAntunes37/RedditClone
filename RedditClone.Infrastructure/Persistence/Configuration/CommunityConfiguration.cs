namespace RedditClone.Infrastructure.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommunityAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public class CommunityConfiguration
 : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> builder)
    {
        ConfigureCommunityTable(builder);
    }

    private static void ConfigureCommunityTable(EntityTypeBuilder<Community> builder)
    {
        builder.ToTable("Communities");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
             .ValueGeneratedNever()
             .HasColumnName("Id")
             .HasConversion(id => id.Value,
                 value => new CommunityId(value));

        builder.Property(c => c.Name)
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .HasMaxLength(200);

        builder.Property(c => c.Topic)
            .HasMaxLength(100);

        builder.Property(c => c.CreatedAt);

        builder.Property(c => c.UpdatedAt);

        builder.HasIndex(c => c.Name)
            .IsUnique();
    }
}