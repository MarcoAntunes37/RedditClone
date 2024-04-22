namespace RedditClone.Tests.ApplicationTests.UserCommunities.Commands;

using ErrorOr;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Application.UserCommunities.Commands.UserJoinACommunity;
using RedditClone.Application.UserCommunities.Commands.UserCommunityRoleUpdate;
using RedditClone.Application.UserCommunities.Results.UserCommunityRoleUpdateResult;
using RedditClone.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using RedditClone.Infrastructure.Persistence;

public class UserCommunityRoleUpdateCommandHandlerTests
{
    [Fact]
    public async void UserCommunityRoleUpdateCommand_ShouldReturnUserCommunityRoleUpdateCommand_WhenUserCommunitiesIsValid()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

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

            var handler = new UserCommunityRoleUpdateCommandHandler(userCommunitiesRepository);

            var command = new UserCommunityRoleUpdateCommand(
                communityId,
                userId,
                Role.Member);

            var result = await handler.Handle(command, default);

            await context.SaveChangesAsync();

            Assert.IsType<ErrorOr<UserCommunityRoleUpdateResult>>(result);

            Assert.Equal(Role.Member, userCommunities.Role);
        }
    }
}