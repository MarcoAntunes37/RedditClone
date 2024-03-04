namespace RedditClone.Infrastructure.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate;

public class UserCommunitiesConfiguration : IEntityTypeConfiguration<UserCommunities>
{
    public void Configure(EntityTypeBuilder<UserCommunities> builder)
    {
        builder.ToTable("UserCommunities");

        builder.HasKey(uc => new { uc.UserId, uc.CommunityId });

        builder.Property(uc => uc.UserId)
            .ValueGeneratedNever()
            .HasColumnName("UserId")
            .HasConversion(Id => Id.Value,
                value => new UserId(value));

        builder.HasOne<User>()
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(uc => uc.CommunityId)
            .ValueGeneratedNever()
            .HasColumnName("CommunityId")
            .HasConversion(Id => Id.Value,
                value => new CommunityId(value));

        builder.HasOne<Community>()
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}