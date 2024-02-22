namespace RedditClone.Infrastructure.Persistence.Configuration;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class CommentConfiguration
 : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        ConfigureCommentsTable(builder);
        ConfigureCommentsVotesTable(builder);
        ConfigureCommentRepliesTable(builder);
    }

    private void ConfigureCommentRepliesTable(EntityTypeBuilder<Comment> builder)
    {
        builder.OwnsMany(cr => cr.Replies, crb =>
        {
            crb.ToTable("CommentsReplies");

            crb.HasKey("Id");

            crb.WithOwner()
                .HasForeignKey();

            crb.Property(cr => cr.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => new ReplyId(value));

            crb.Property(cr => cr.UserId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => new UserId(value));

            crb.HasOne<User>()
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            crb.Property(cr => cr.CommentId)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value,
                    value => new CommentId(value));

            crb.Property(cr => cr.Content);

            crb.Property(cr => cr.CreatedAt);

            crb.Property(cr => cr.UpdatedAt);

            crb.OwnsMany(rv => rv.Votes, rvb =>
            {
                rvb.ToTable("RepliesVotes");

                rvb.HasKey("Id");

                rvb.WithOwner()
                    .HasForeignKey("ReplyId");

                rvb.Property(rv => rv.Id)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value,
                        value => new VoteId(value));

                rvb.Property(rv => rv.UserId)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value,
                        value => new UserId(value));

                rvb.HasOne<User>()
                    .WithMany()
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                rvb.Property(rv => rv.ReplyId)
                    .ValueGeneratedNever()
                    .HasConversion(id => id.Value,
                        value => new ReplyId(value));

                rvb.Property(rv => rv.IsVoted);
            });
        });
    }

    private void ConfigureCommentsVotesTable(EntityTypeBuilder<Comment> builder)
    {
        builder.OwnsMany(cv => cv.Votes, cvb =>
        {
            cvb.ToTable("CommentsVotes");

            cvb.WithOwner()
                .HasForeignKey();

            cvb.HasKey("Id");

            cvb.Property(cv => cv.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => new VoteId(value));

            cvb.Property(cv => cv.CommentId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => new CommentId(value));

            cvb.HasOne<Post>()
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            cvb.Property(cv => cv.UserId)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value,
                value => new UserId(value));

            cvb.Property(cv => cv.IsVoted);

            cvb.HasIndex(cv => new { cv.UserId, cv.CommentId })
                .IsUnique();
        });
    }

    private void ConfigureCommentsTable(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => new CommentId(value));

        builder.Property(c => c.UserId)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => new UserId(value));

        builder.HasOne<User>()
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.PostId)
             .ValueGeneratedNever()
             .HasConversion(id => id.Value,
                 value => new PostId(value));

        builder.HasOne<Post>()
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.Content)
            .HasMaxLength(255);

        builder.Property(c => c.CreatedAt);

        builder.Property(c => c.UpdatedAt);
    }
}