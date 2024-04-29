namespace RedditClone.Domain.CommentAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;


public record CommentUpdatedDomainEvent(
    Guid Id,
    CommentId CommentId,
    PostId PostId,
    UserId UserId,
    string Content,
    DateTime UpdatedAt
) : IDomainEvent;