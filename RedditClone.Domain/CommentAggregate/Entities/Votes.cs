namespace RedditClone.Domain.CommentAggregate.Entities;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.DomainEvents;
using RedditClone.Domain.CommentAggregate.ValueObjects;

public sealed class Votes : AggregateRoot
{
    public new VoteId Id { get; private set; }
    public UserId UserId { get; private set; }
    public CommentId CommentId { get; private set; }
    public bool IsVoted { get; private set; }

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
        var vote = new Votes(
            new VoteId(Guid.NewGuid()),
            commentId,
            userId,
            isVoted);

        vote.RaiseDomainEvent(
            new VoteCreatedDomainEvent(
                Guid.NewGuid(),
                vote.Id,
                vote.CommentId,
                vote.UserId,
                isVoted));

        return vote;
    }

    public void UpdateVote(bool isVoted)
    {
        IsVoted = isVoted;

        this.RaiseDomainEvent(
            new VoteUpdatedDomainEvent(
                Guid.NewGuid(),
                this.Id,
                CommentId,
                UserId,
                IsVoted));
    }

    public void DeleteVote()
    {
        this.RaiseDomainEvent(
            new VoteDeletedDomainEvent(
                Guid.NewGuid(),
                this.Id,
                CommentId,
                UserId));
    }
}