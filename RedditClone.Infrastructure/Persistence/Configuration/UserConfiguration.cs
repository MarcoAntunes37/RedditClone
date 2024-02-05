using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.Infrastructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<UserAggregate>
{
    public void Configure(EntityTypeBuilder<UserAggregate> builder)
    {
        ConfigureUserTable(builder);
    }

    private void ConfigureUserTable(EntityTypeBuilder<UserAggregate> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(m => m.Id);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasColumnName("UserId")
            .HasConversion(Id => Id.Value,
                value => UserId.Create(value));

        builder.Property(m => m.FirstName)
            .HasMaxLength(100);

        builder.Property(m => m.LastName)
            .HasMaxLength(100);

        builder.Property(m => m.Username)
            .HasMaxLength(100);

        builder.Property(m => m.Password)
            .HasMaxLength(100);

        builder.Property(m => m.CreatedAt);

        builder.Property(m => m.UpdatedAt);
    }
}