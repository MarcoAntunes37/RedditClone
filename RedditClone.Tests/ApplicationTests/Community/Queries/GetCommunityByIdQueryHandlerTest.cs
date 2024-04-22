namespace RedditClone.Tests.ApplicationTests.Community.Queries;

using Moq;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Comment.Queries.GetCommunityById;
using RedditClone.Application.Community.Queries.GetCommunitiesById;
using RedditClone.Application.Community.Results.GetCommunityByIdResult;

public class GetCommunityByIdQueryHandlerTest
{
    [Fact]
    public async void GetCommunityByIdQuery_ShouldReturnGetCommunityByIdResult_WhenCommunityIsValid()
    {
        var communityRepositoryMock = new Mock<ICommunityRepository>();

        var handler = new GetCommunityByIdQueryHandler(communityRepositoryMock.Object);

        var query = new GetCommunityByIdQuery(new CommunityId(Guid.NewGuid()));

        var result = await handler.Handle(query, default);

        Assert.IsType<GetCommunityByIdResult>(result);

        communityRepositoryMock.Verify(r => r.GetCommunityById(It.IsAny<CommunityId>()), Times.Once);
    }
}