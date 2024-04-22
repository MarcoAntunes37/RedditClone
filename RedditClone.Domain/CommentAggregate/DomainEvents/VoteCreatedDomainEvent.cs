namespace RedditClone.Domain.CommentAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;

public record VoteCreatedDomainEvent(
    Guid Id,
    VoteId VoteId,
    CommentId CommentId,
    UserId UserId,
    bool IsVoted
) : IDomainEvent;