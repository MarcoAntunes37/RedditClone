namespace RedditClone.Infrastructure.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public class UserCommunitiesConfiguration : IEntityTypeConfiguration<UserCommunities>
{
    public void Configure(EntityTypeBuilder<UserCommunities> builder)
    {
        builder.ToTable("UserCommunities");

        builder.Ignore(uc => uc.Id);

        builder.HasKey(uc => new { uc.UserId, uc.CommunityId });

        builder.Property(uc => uc.UserId)
            .ValueGeneratedNever()
            .HasColumnName("UserId")
            .HasConversion(Id => Id.Value,
                value => new UserId(value));

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey("UserId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(uc => uc.CommunityId)
            .ValueGeneratedNever()
            .HasColumnName("CommunityId")
            .HasConversion(Id => Id.Value,
                value => new CommunityId(value));

        builder.HasOne<Community>()
            .WithMany()
            .HasForeignKey("CommunityId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(uc => uc.Role)
            .HasConversion(
                v => v.ToString(),
                v => (Role)Enum.Parse(typeof(Role), v));
    }
}