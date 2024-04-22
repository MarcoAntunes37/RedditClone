namespace RedditClone.Tests.ApplicationTests.Community.Commands;

using Moq;
using ErrorOr;
using RedditClone.Application.Persistence;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Application.Community.Results.UpdateCommunityResult;
using RedditClone.Application.Community.Commands.UpdateCommunity;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;

public class UpdateCommunityCommandHandlerTest
{
    [Fact]
    public async void UpdateCommand_ShouldReturnUpdateResult_WhenUserIsValid()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var communityRepositoryMock = new Mock<ICommunityRepository>();

        using (var context = new RedditCloneDbContext(options))
        {
            var userCommunitiesRepository = new UserCommunitiesRepository(context);

            var handler = new UpdateCommunityCommandHandler(
                        communityRepositoryMock.Object,
                        userCommunitiesRepository);

            var communityId = new CommunityId(Guid.NewGuid());

            var userId = new UserId(Guid.NewGuid());

            var userCommunities = UserCommunities.Create(
                userId,
                communityId,
                Role.Admin);

            context.UserCommunities.Add(userCommunities);

            await context.SaveChangesAsync();

            var command = new UpdateCommunityCommand(
                communityId,
                userId,
                "Dev",
                "Test",
                "TestCommunity");

            var result = await handler.Handle(command, default);

            Assert.IsType<ErrorOr<UpdateCommunityResult>>(result);

            communityRepositoryMock.Verify(r => r.UpdateCommunityById(
                It.IsAny<CommunityId>(),
                It.IsAny<UserId>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()), Times.Once);
        }
    }
}