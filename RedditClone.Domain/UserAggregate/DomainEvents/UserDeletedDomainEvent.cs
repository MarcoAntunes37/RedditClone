namespace RedditClone.Domain.UserAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed record UserDeletedDomainEvent(Guid Id, UserId UserId) : IDomainEvent;