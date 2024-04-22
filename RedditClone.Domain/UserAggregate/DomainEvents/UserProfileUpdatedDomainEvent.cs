namespace RedditClone.Domain.UserAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed record UserProfileUpdatedDomainEvent(
    Guid Id,
    UserId UserId,
    string Firstname,
    string Lastname,
    string Email,
    DateTime UpdatedAt) : IDomainEvent;