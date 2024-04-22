namespace RedditClone.Domain.CommentAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record ReplyCreatedDomainEvent(
    Guid Id,
    ReplyId ReplyId,
    CommentId CommentId,
    CommunityId CommunityId,
    UserId UserId,
    string Content
) : IDomainEvent;