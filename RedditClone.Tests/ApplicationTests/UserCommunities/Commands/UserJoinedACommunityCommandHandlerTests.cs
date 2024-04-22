namespace RedditClone.Tests.ApplicationTests.UserCommunities.Commands;

using Moq;
using ErrorOr;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Commands.UserJoinACommunity;
using RedditClone.Application.UserCommunities.Results.UserJoinACommunityResults;

public class UserJoinACommunityCommandHandlerTests
{
    [Fact]
    public async void UserJoinACommunityCommand_ShouldReturnUserJoinACommunityResult_WhenUserCommunitiesIsValid()
    {
        var userCommunitiesRepositoryMock = new Mock<IUserCommunitiesRepository>();

        var handler = new UserJoinACommunityCommandHandler(
            userCommunitiesRepositoryMock.Object);

        var command = new UserJoinACommunityCommand(
            new CommunityId(Guid.NewGuid()),
            new UserId(Guid.NewGuid()),
            Role.Admin);

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<UserJoinACommunityResult>>(result);

        userCommunitiesRepositoryMock.Verify(r => r.Add(It.IsAny<UserCommunities>()), Times.Once);
    }
}