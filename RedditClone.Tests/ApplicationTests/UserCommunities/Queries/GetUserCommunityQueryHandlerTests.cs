namespace RedditClone.Tests.ApplicationTests.UserCommunities.Queries;

using Moq;
using ErrorOr;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Queries.GetUserCommunity;
using RedditClone.Application.UserCommunities.Results.GetUserCommunityResults;

public class GetUserCommunityQueryHandlerTests
{
    [Fact]
    public async Task GetUserCommunityQuery_ShouldReturnGetUserCommunitiesResult_WhenUserIsValid()
    {
        var mockUserCommunitiesRepository = new Mock<IUserCommunitiesRepository>();

        var handler = new GetUserCommunityQueryHandler(mockUserCommunitiesRepository.Object);

        var query = new GetUserCommunityQuery(new UserId(Guid.NewGuid()), new CommunityId(Guid.NewGuid()));

        var result = await handler.Handle(query, default);

        Assert.IsType<ErrorOr<GetUserCommunityResult>>(result);

        mockUserCommunitiesRepository.Verify(r => r.GetUserCommunities(It.IsAny<UserId>(), It.IsAny<CommunityId>()), Times.Once);
    }
}