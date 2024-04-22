namespace RedditClone.Domain.PostAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public sealed record PostCreatedDomainEvent(
    Guid Id,
    PostId PostId,
    CommunityId CommunityId,
    UserId UserId) : IDomainEvent;