namespace RedditClone.Domain.CommunityAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public sealed record CommunityCreatedDomainEvent(Guid Id, CommunityId CommunityId, UserId UserId, string Name) : IDomainEvent;