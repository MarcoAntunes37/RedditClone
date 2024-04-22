namespace RedditClone.Domain.UserCommunitiesAggregate;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.DomainEvents;

public sealed class UserCommunities : AggregateRoot
{
    public UserId UserId { get; private set; }
    public CommunityId CommunityId { get; private set; }
    public Role Role { get; private set; }

#pragma warning disable CS8618
    private UserCommunities() { }
#pragma warning restore

    private UserCommunities(UserId userId, CommunityId communityId, Role role)
    {
        UserId = userId;
        CommunityId = communityId;
        Role = role;
    }

    public static UserCommunities Create(UserId userId, CommunityId communityId, Role role)
    {
        var userCommunities = new UserCommunities(
            userId,
            communityId,
            role
        );

        userCommunities.RaiseDomainEvent(
            new UserJoinedCommunityDomainEvent(
                Guid.NewGuid(),
                userCommunities.UserId,
                userCommunities.CommunityId,
                userCommunities.Role));

        return userCommunities;
    }

    public void UpdateRole(Role role)
    {
        var oldRole = Role;
        Role = role;

        RaiseDomainEvent(
            new RoleUpdatedCommunityDomainEvent(
                Guid.NewGuid(),
                UserId,
                CommunityId,
                oldRole,
                role));
    }

    public void Delete()
    {
        RaiseDomainEvent(
            new UserLeftCommunityDomainEvent(
                Guid.NewGuid(),
                UserId,
                CommunityId,
                Role));
    }
}