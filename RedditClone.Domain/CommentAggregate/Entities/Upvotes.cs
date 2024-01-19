using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommentAggregate.ValueObjects;

namespace RedditClone.Domain.CommentAggregate.Entities;

public sealed class Upvotes : Entity<UpvoteId>
{
    public UserId UserId;

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