namespace RedditClone.Tests.ApplicationTests.Community.Commands;

using Moq;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RedditClone.Application.Persistence;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.Community.Commands.DeleteCommunity;
using RedditClone.Application.Community.Results.DeleteCommunityResult;

public class DeleteCommunityCommandHandlerTest
{
    [Fact]
    public async void DeleteCommand_ShouldReturnDeleteResult_WhenUserIsValid()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var communityRepositoryMock = new Mock<ICommunityRepository>();

        var userId = new UserId(Guid.NewGuid());

        var communityId = new CommunityId(Guid.NewGuid());

        var userCommunities = UserCommunities.Create(
            userId,
            communityId,
            Role.Admin);

        using (var context = new RedditCloneDbContext(options))
        {
            var userCommunitiesRepository = new UserCommunitiesRepository(context);

            context.UserCommunities.Add(userCommunities);

            await context.SaveChangesAsync();

            var handler = new DeleteCommunityCommandHandler(
                communityRepositoryMock.Object,
                userCommunitiesRepository);

            var command = new DeleteCommunityCommand(
                communityId,
                userId);

            var result = await handler.Handle(command, default);

            Assert.IsType<ErrorOr<DeleteCommunityResult>>(result);

            communityRepositoryMock.Verify(r => r.DeleteCommunityById(It.IsAny<CommunityId>(), It.IsAny<UserId>()), Times.Once);
        }
    }
}