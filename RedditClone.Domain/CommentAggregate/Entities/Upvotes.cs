using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommentAggregate.ValueObjects;

namespace RedditClone.Domain.CommentAggregate.Entities;

public sealed class Upvotes : Entity<UpvoteId>
{
    public UserId UserId;

#pragma warning disable CS8618
    private Upvotes() { }
#pragma warning restore CS8618

    private Upvotes(UpvoteId upvoteId,
    UserId userId) : base(upvoteId)
    {
        UserId = userId;
    }

    public static Upvotes Create(
        UserId userId)
    {
        return new(
            UpvoteId.CreateUnique(),
            userId);
    }
}