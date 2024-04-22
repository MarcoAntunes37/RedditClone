namespace RedditClone.Tests.InfrastructureTests.Repository;

using System;
using Microsoft.EntityFrameworkCore;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;

public class UserCommunitiesRepositoryTests
{
    [Fact]
    public void UserCommunitiesRepository_ShouldUserJoinACommunity_WhenUserCommunitiesIsValid()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var userCommunities = arrange["userCommunities"] as UserCommunities;

        using (var context = new RedditCloneDbContext(options!))
        {
            var userCommunitiesRepository = new UserCommunitiesRepository(context);

            userCommunitiesRepository.Add(userCommunities!);

            context.SaveChanges();

            Assert.Equal(1, context.UserCommunities.Count());
        }
    }

    [Fact]
    public void UserCommunitiesRepository_ShouldUserCommunityRoleUpdate_WhenUserCommunitiesExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var userCommunities = arrange["userCommunities"] as UserCommunities;

        using (var context = new RedditCloneDbContext(options!))
        {
            var userCommunitiesRepository = new UserCommunitiesRepository(context);

            userCommunitiesRepository.Add(userCommunities!);

            context.SaveChanges();

            userCommunitiesRepository.UpdateRole(
                userCommunities!.UserId,
                userCommunities.CommunityId,
                (Role)arrange["role"]);

            context.SaveChanges();

            Assert.Equal(1, context.UserCommunities.Count());

            Assert.Equal((Role)arrange["role"], context.UserCommunities.First().Role);
        }

    }

    [Fact]
    public void UserCommunitiesRepository_ShouldDeleteUserCommunities_WhenUserCommunitiesExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var userCommunities = arrange["userCommunities"] as UserCommunities;

        using (var context = new RedditCloneDbContext(options!))
        {
            var userCommunitiesRepository = new UserCommunitiesRepository(context);

            userCommunitiesRepository.Add(userCommunities!);

            context.SaveChanges();

            userCommunitiesRepository.Remove(userCommunities!.UserId, userCommunities.CommunityId);

            context.SaveChanges();

            Assert.Equal(0, context.UserCommunities.Count());
        }
    }

    private static Dictionary<string, object> CreateTestArranges()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var userCommunities = UserCommunities.Create(
            new UserId(Guid.NewGuid()),
            new CommunityId(Guid.NewGuid()),
            Role.Admin);

        return new Dictionary<string, object>()
        {
            { "userCommunities", userCommunities },
            { "options", options },
            { "role", Role.Member }
        };
    }
}