namespace RedditClone.Infrastructure.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;
using RedditClone.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        builder.HasKey(x => x.Id);
    }
}