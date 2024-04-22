namespace RedditClone.Infrastructure.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.UserAggregate.ValueObjects;


public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUserTable(builder);
    }

    private void ConfigureUserTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasColumnName("Id")
            .HasConversion(Id => Id.Value,
                value => new UserId(value));

        builder.Property(m => m.Firstname)
            .HasMaxLength(100);

        builder.Property(m => m.Lastname)
            .HasMaxLength(100);

        builder.Property(m => m.Username)
            .HasMaxLength(100);

        builder.Property(m => m.Password)
            .HasMaxLength(100);

        builder.Property(m => m.CreatedAt);

        builder.Property(m => m.UpdatedAt);
    }
}