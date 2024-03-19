namespace RedditClone.Infrastructure.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class CommunityConfiguration
 : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> builder)
    {
        ConfigureCommunityTable(builder);
    }

    private void ConfigureCommunityTable(EntityTypeBuilder<Community> builder)
    {
        builder.ToTable("Communities");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
             .ValueGeneratedNever()
             .HasColumnName("Id")
             .HasConversion(id => id.Value,
                 value => new CommunityId(value));

        builder.Property(c => c.UserId)
            .ValueGeneratedNever()
            .HasColumnName("UserId")
            .HasConversion(id => id.Value,
            value => new UserId(value));

        builder.HasOne<User>()
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

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