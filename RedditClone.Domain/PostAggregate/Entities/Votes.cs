namespace RedditClone.Domain.PostAggregate.Entities;

using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class Votes
{
    public VoteId Id { get; private set; }
    public UserId UserId { get; private set; }
    public PostId PostId { get; private set; }
    public bool IsVoted { get; private set; }

#pragma warning disable CS8618
    private Votes() { }
#pragma warning restore CS8618
    private Votes(
        VoteId id,
        PostId postId,
        UserId userId,
        bool isVoted)
    {
        Id = id;
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
            new VoteId(Guid.NewGuid()),
            postId,
            userId,
            isVoted);
    }

    public void UpdateVote(bool isVoted)
    {
        IsVoted = isVoted;
    }
}