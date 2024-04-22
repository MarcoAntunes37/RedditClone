namespace RedditClone.Domain.CommentAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public record ReplyUpdatedDomainEvent(
    Guid Id,
    ReplyId ReplyId,
    CommentId CommentId,
    CommunityId CommunityId,
    UserId UserId,
    string Content,
    DateTime UpdatedAt
) : IDomainEvent;