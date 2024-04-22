namespace RedditClone.Domain.CommunityAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public sealed record CommunityDeletedDomainEvent(Guid Id, CommunityId CommunityId, string Name) : IDomainEvent;