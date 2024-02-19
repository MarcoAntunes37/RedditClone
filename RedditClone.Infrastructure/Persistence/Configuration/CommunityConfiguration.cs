namespace RedditClone.Infrastructure.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate;

public class CommunityConfiguration
 : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> builder)
    {
        ConfigureCommunityTable(builder);
        builder.HasOne<User>()
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureCommunityTable(EntityTypeBuilder<Community> builder)
    {
        builder.ToTable("Communities");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
             .ValueGeneratedNever()
             .HasColumnName("Id")
             .HasConversion(id => id.Value,
                 value => CommunityId.Create(value));

        builder.Property(c => c.UserId)
            .ValueGeneratedNever()
            .HasColumnName("UserId")
            .HasConversion(id => id.Value,
            value => UserId.Create(value));

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