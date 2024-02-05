using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

namespace RedditClone.Infrastructure.Persistence.Configuration;

public class CommunityConfiguration
 : IEntityTypeConfiguration<CommunityAggregate>
{
    public void Configure(EntityTypeBuilder<CommunityAggregate> builder)
    {
        ConfigureCommunityTable(builder);
    }

    private void ConfigureCommunityTable(EntityTypeBuilder<CommunityAggregate> builder)
    {
        builder.ToTable("Communities");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
             .ValueGeneratedNever()
             .HasColumnName("CommunityId")
             .HasConversion(id => id.Value,
                 value => CommunityId.Create(value));

        builder.Property(c => c.Name)
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .HasMaxLength(200);

        builder.Property(c => c.Topic)
            .HasMaxLength(100);

        builder.Property(c => c.CreatedAt);

        builder.Property(c => c.UpdatedAt);
    }
}