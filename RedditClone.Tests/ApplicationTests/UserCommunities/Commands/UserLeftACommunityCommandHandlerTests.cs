namespace RedditClone.Tests.ApplicationTests.UserCommunities.Commands;

using Moq;
using ErrorOr;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Results.UserJoinACommunityResults;
using RedditClone.Application.UserCommunities.Commands.UserLeftACommunity;
using Microsoft.EntityFrameworkCore;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.UserCommunitiesAggregate;

public class UserLeftACommunityCommandHandlerTests
{
    [Fact]
    public async void UserLeftACommunityCommand_ShouldReturnUserLeftACommunityResult_WhenUserCommunitiesIsValid()
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

            var handler = new UserLeftACommunityCommandHandler(userCommunitiesRepository);

            var command = new UserLeftACommunityCommand(
                communityId,
                userId);

            var result = await handler.Handle(command, default);

            await context.SaveChangesAsync();

            Assert.IsType<ErrorOr<UserLeftACommunityResult>>(result);

            Assert.Equal(0, context.UserCommunities.Count());
        }
    }
}