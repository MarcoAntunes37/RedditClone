namespace RedditClone.Domain.PostAggregate.Entities;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.DomainEvents;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class Votes : AggregateRoot
{
    public new VoteId Id { get; private set; }
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
        var vote = new Votes(
            new VoteId(Guid.NewGuid()),
            postId,
            userId,
            isVoted);

        vote.RaiseDomainEvent(
            new VoteCreatedDomainEvent(
                Guid.NewGuid(),
                vote.Id,
                vote.PostId,
                vote.UserId,
                vote.IsVoted));

        return vote;
    }

    public void UpdateVote(bool isVoted)
    {
        IsVoted = isVoted;

        this.RaiseDomainEvent(
            new VoteUpdatedDomainEvent(
                Guid.NewGuid(),
                this.Id,
                PostId,
                UserId,
                IsVoted));
    }

    public void DeleteVote()
    {
        this.RaiseDomainEvent(
            new VoteDeletedDomainEvent(
                Guid.NewGuid(),
                this.Id,
                PostId,
                UserId));
    }
}