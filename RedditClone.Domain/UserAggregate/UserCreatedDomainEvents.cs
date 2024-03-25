namespace RedditClone.Domain.UserAggregate;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record UserCreatedDomainEvent(Guid Id, UserId UserId) : DomainEvent(Id);
