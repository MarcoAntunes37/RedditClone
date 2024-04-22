namespace RedditClone.Tests.ApplicationTests.Community.Commands;

using Moq;
using ErrorOr;
using Domain.CommunityAggregate;
using RedditClone.Application.Persistence;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Community.Commands.CreateCommunity;
using RedditClone.Application.Community.Results.CreateCommunityResult;

public class CreateCommunityCommandHandlerTest
{
    [Fact]
    public async void CreateCommand_ShouldReturnCreateResult_WhenUserIsValid()
    {
        var communityRepositoryMock = new Mock<ICommunityRepository>();

        var userCommunitiesRepositoryMock = new Mock<IUserCommunitiesRepository>();

        var handler = new CreateCommunityCommandHandler(
            communityRepositoryMock.Object,
            userCommunitiesRepositoryMock.Object);

        var command = new CreateCommunityCommand(
            "Dev",
            "Test",
            "TestCommunity",
            new UserId(Guid.NewGuid()));

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<CreateCommunityResult>>(result);

        communityRepositoryMock.Verify(r => r.Add(It.IsAny<Community>()), Times.Once);

        userCommunitiesRepositoryMock.Verify(r => r.Add(It.IsAny<UserCommunities>()), Times.Once);
    }
}