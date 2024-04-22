namespace RedditClone.Domain.UserCommunitiesAggregate.DomainEvents;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public record UserLeftCommunityDomainEvent(
    Guid Id,
    UserId UserId,
    CommunityId CommunityId,
    Role Role) : IDomainEvent;