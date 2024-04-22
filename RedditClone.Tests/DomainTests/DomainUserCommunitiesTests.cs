namespace RedditClone.Tests.DomainTests;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.DomainEvents;

public class DomainUserCommunitiesTests
{
    [Fact]
    public static void User_Join_Community_ValidInput_Success()
    {
        Guid userId = Guid.NewGuid();
        Guid communityId = Guid.NewGuid();
        Role role = Role.Member;
        int eventCounter = 0;

        var userCommunities = UserCommunities.Create(
            new UserId(userId),
            new CommunityId(communityId),
            role);

        eventCounter++;

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)userCommunities.GetDomainEvents();

        Assert.NotNull(userCommunities);
        Assert.IsType<UserJoinedCommunityDomainEvent>(domainEvents[eventCounter - 1]);
        Assert.Equal(new UserId(userId), userCommunities.UserId);
        Assert.Equal(new CommunityId(communityId), userCommunities.CommunityId);
    }

    [Fact]
    public void UpdateRole_UserCommunities_ValidInput_Success()
    {
        var userId = new UserId(Guid.NewGuid());
        var communityId = new CommunityId(Guid.NewGuid());
        var newRole = Role.Moderator;
        int eventCounter = 0;
        var userCommunities = UserCommunities.Create(
            userId,
            communityId,
            Role.Member);

        eventCounter++;

        userCommunities.UpdateRole(newRole);

        eventCounter++;

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)userCommunities.GetDomainEvents();

        Assert.NotNull(userCommunities);
        Assert.IsType<RoleUpdatedCommunityDomainEvent>(domainEvents[eventCounter - 1]);
        Assert.Equal(newRole, userCommunities.Role);
    }

    [Fact]
    public void User_Left_Community_ValidInput_Success()
    {
        var userId = new UserId(Guid.NewGuid());
        var communityId = new CommunityId(Guid.NewGuid());
        int eventCounter = 0;
        var userCommunities = UserCommunities.Create(
            userId,
            communityId,
            Role.Member);

        eventCounter++;

        userCommunities.Delete();

        eventCounter++;

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)userCommunities.GetDomainEvents();

        Assert.NotNull(userCommunities);
        Assert.IsType<UserLeftCommunityDomainEvent>(domainEvents[eventCounter - 1]);
    }
}