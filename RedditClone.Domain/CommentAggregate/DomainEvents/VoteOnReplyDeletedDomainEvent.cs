namespace RedditClone.Domain.CommentAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;

public record VoteOnReplyDeletedDomainEvent(
    Guid Id,
    VoteId VoteId,
    ReplyId ReplyId,
    UserId UserId
) : IDomainEvent;