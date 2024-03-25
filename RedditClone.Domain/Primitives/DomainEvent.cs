namespace RedditClone.Domain.Primitives;

using MediatR;

public record DomainEvent(Guid Id) : INotification;