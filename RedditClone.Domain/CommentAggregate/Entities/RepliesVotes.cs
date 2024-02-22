namespace RedditClone.Domain.CommentAggregate.Entities;

using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class RepliesVotes
{
    public VoteId Id;
    public UserId UserId;
    public ReplyId ReplyId;
    public bool IsVoted;

#pragma warning disable CS8618
    private RepliesVotes() { }
#pragma warning restore CS8618

    private RepliesVotes(
        VoteId id,
        ReplyId replyId,
        UserId userId,
        bool isVoted)
    {
        Id = id;
        ReplyId = replyId;
        UserId = userId;
        IsVoted = isVoted;
    }

    public static RepliesVotes Create(
        ReplyId replyId,
        UserId userId,
        bool isVoted)
    {
        return new(
            new VoteId(Guid.NewGuid()),
            replyId,
            userId,
            isVoted);
    }
}