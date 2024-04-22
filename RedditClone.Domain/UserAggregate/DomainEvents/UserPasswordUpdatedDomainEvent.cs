namespace RedditClone.Domain.UserAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed record UserPasswordUpdatedDomainEvent(
    Guid Id,
    UserId UserId,
    string Password,
    DateTime UpdatedAt) : IDomainEvent;