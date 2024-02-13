namespace RedditClone.Infrastructure.Persistence.Configuration;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.ValueObjects;

public class CommentConfiguration
 : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        ConfigureCommentsTable(builder);
        ConfigureCommentsVotesTable(builder);
        ConfigureCommentsRepliesTable(builder);
    }

    private void ConfigureCommentsRepliesTable(EntityTypeBuilder<Comment> builder)
    {
        builder.OwnsMany(cr => cr.Replies, crb =>
        {
            crb.ToTable("RepliesComments");

            crb.WithOwner()
                .HasForeignKey();

            crb.HasKey("Id");

            crb.Property(cr => cr.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => ReplyId.Create(value));

            crb.Property(cr => cr.UserId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => UserId.Create(value));

            crb.Property(cr => cr.Username);

            crb.Property(cr => cr.Content);

            crb.Property(cr => cr.CreatedAt);

            crb.Property(cr => cr.UpdatedAt);

            crb.OwnsMany(rv => rv.Votes, rvb => {
                rvb.ToTable("RepliesVotes");

                rvb.HasKey("Id");

                rvb.WithOwner()
                    .HasForeignKey();



                rvb.Property(rv => rv.Id)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value,
                        value => VoteId.Create(value));

                rvb.Property(rv => rv.UserId)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value,
                        value => UserId.Create(value));

                rvb.Property(rv => rv.PostId)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value,
                        value => PostId.Create(value));

                rvb.Property(rv => rv.IsVoted);
            });
        });
    }

    private void ConfigureCommentsVotesTable(EntityTypeBuilder<Comment> builder)
    {
        builder.OwnsMany(cv => cv.Votes, cvb =>
        {
            cvb.ToTable("VotesComments");

            cvb.WithOwner()
                .HasForeignKey();

            cvb.HasKey("Id", "PostId");

            cvb.Property(cv => cv.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => VoteId.Create(value));

            cvb.Property(cv => cv.PostId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => PostId.Create(value));

            cvb.Property(cv => cv.UserId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => UserId.Create(value));

            cvb.Property(cv => cv.IsVoted);
        });
    }

    private void ConfigureCommentsTable(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => CommentId.Create(value));

        builder.Property(c => c.UserId)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => UserId.Create(value));

        builder.Property(c => c.PostId)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => PostId.Create(value));

        builder.Property(c => c.Content)
            .HasMaxLength(255);

        builder.Property(c => c.CreatedAt);

        builder.Property(c => c.UpdatedAt);
    }
}