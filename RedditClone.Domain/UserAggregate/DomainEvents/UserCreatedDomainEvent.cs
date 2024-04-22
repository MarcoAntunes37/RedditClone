namespace RedditClone.Domain.UserAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed record UserCreatedDomainEvent(
    Guid Id,
    UserId UserId,
    string Email,
    string Username) : IDomainEvent;