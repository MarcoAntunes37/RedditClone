using RedditClone.Domain.Common.Models;
using RedditClone.Domain.Post.ValueObjects;

namespace RedditClone.Domain.User.Entities;

public sealed class Downvotes : 
Entity<DownvoteId>
{
    public UserId UserId;

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