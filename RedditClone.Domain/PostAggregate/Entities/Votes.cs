namespace RedditClone.Domain.PostAggregate.Entities;

using RedditClone.Domain.Common.Models;
using RedditClone.Domain.PostAggregate.ValueObjects;

public sealed class Votes
    : Entity<VoteId>
{
    public UserId UserId;
    public PostId PostId;
    public bool IsVoted;

#pragma warning disable CS8618
    private Votes() { }
#pragma warning restore CS8618

    private Votes(
        VoteId voteId,
        PostId postId,
        UserId userId,
        bool isVoted) : base(voteId)
    {
        PostId = postId;
        UserId = userId;
        IsVoted = isVoted;
    }

    public static Votes Create(
        PostId postId,
        UserId userId,
        bool isVoted)
    {
        return new(
            VoteId.CreateUnique(),
            postId,
            userId,
            isVoted);
    }
}