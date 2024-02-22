namespace RedditClone.Domain.CommentAggregate.Entities;

using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class Votes
{
    public VoteId Id;
    public UserId UserId;
    public CommentId CommentId;
    public bool IsVoted;

#pragma warning disable CS8618
    private Votes() { }
#pragma warning restore CS8618

    private Votes(
        VoteId id,
        CommentId commentId,
        UserId userId,
        bool isVoted)
    {
        Id = id;
        CommentId = commentId;
        UserId = userId;
        IsVoted = isVoted;
    }

    public static Votes Create(
        CommentId commentId,
        UserId userId,
        bool isVoted)
    {
        return new(
            new VoteId(Guid.NewGuid()),
            commentId,
            userId,
            isVoted);
    }
}