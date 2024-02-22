namespace RedditClone.Domain.PostAggregate.Entities;

using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class Votes
{
    public VoteId Id;
    public UserId UserId;
    public PostId PostId;
    public bool IsVoted;

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
}