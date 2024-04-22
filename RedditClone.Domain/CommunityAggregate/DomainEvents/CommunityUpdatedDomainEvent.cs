namespace RedditClone.Domain.CommunityAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public sealed record CommunityUpdatedDomainEvent(
    Guid Id,
    CommunityId CommunityId,
    string Name,
    string Description,
    string Topic,
    DateTime UpdatedAt) : IDomainEvent;