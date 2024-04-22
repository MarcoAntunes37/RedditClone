namespace RedditClone.Domain.UserCommunitiesAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public record RoleUpdatedCommunityDomainEvent(
    Guid Id,
    UserId UserId,
    CommunityId CommunityId,
    Role OldRole,
    Role Role) : IDomainEvent;
