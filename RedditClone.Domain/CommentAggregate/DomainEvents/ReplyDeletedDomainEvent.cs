namespace RedditClone.Domain.CommentAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;


public record ReplyDeletedDomainEvent(
    Guid Id,
    ReplyId ReplyId,
    CommentId CommentId,
    UserId UserId
) : IDomainEvent;