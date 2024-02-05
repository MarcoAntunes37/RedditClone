using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommentAggregate.ValueObjects;

namespace RedditClone.Domain.CommentAggregate.Entities;

public sealed class Downvotes :
Entity<DownvoteId>
{
    public UserId UserId;

#pragma warning disable CS8618
    private Downvotes() { }
#pragma warning restore CS8618

    private Downvotes(DownvoteId downvoteId,
    UserId userId) : base(downvoteId)
    {
        UserId = userId;
    }

    public static Downvotes Create(
        UserId userId)
    {
        return new(
            DownvoteId.CreateUnique(),
            userId);
    }
}