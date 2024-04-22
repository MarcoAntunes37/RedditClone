namespace RedditClone.Domain.CommentAggregate.Entities;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.CommentAggregate.DomainEvents;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class RepliesVotes : AggregateRoot
{
    public new VoteId Id { get; private set; }
    public UserId UserId { get; private set; }
    public ReplyId ReplyId { get; private set; }
    public bool IsVoted { get; private set; }

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
        var replyVote = new RepliesVotes(
            new VoteId(Guid.NewGuid()),
            replyId,
            userId,
            isVoted);

        replyVote.RaiseDomainEvent(
            new VoteOnReplyCreatedDomainEvent(
                Guid.NewGuid(),
                replyVote.Id,
                replyVote.ReplyId,
                replyVote.UserId,
                replyVote.IsVoted));

        return replyVote;
    }

    public void UpdateReplyVote(bool isVoted)
    {
        IsVoted = isVoted;

        this.RaiseDomainEvent(
            new VoteOnReplyUpdatedDomainEvent(
                Guid.NewGuid(),
                Id,
                ReplyId,
                UserId,
                IsVoted));
    }

    public void DeleteReplyVote()
    {
        this.RaiseDomainEvent(
            new VoteOnReplyDeletedDomainEvent(
                Guid.NewGuid(),
                Id,
                ReplyId,
                UserId));
    }
}