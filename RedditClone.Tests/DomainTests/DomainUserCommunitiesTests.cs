using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate;

namespace RedditClone.Tests.DomainTests;

public class DomainUserCommunitiesTests
{
    public void ShouldReturnUserCommunitiesObjectOnCreate()
    {
        Guid userId = new("7e6e6081-8a44-4f2c-bf97-0e1f0be368e2");
        Guid communityId = new("031abd7e-329b-4d9a-aff4-3b11f468afa2");

        var userCommunities = UserCommunities.Create(
            new UserId(userId),
            new CommunityId(communityId)
        );

        Assert.NotNull(userCommunities);
        Assert.Equal(new UserId(userId), userCommunities.UserId);
        Assert.Equal(new CommunityId(communityId), userCommunities.CommunityId);
    }
}