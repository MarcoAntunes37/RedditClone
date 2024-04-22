namespace RedditClone.Domain.PostAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record VoteUpdatedDomainEvent(
    Guid Id,
    VoteId VoteId,
    PostId PostId,
    UserId UserId,
    bool IsVoted) : IDomainEvent;