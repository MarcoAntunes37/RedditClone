namespace RedditClone.Tests.InfrastructureTests.Repository;

using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Infrastructure.Persistence.Repositories;

public class CommunityRepositoryTests
{
    [Fact]
    public void GetCommunityById_ShouldReturnCommunity_WhenCommunityExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var community = arrange["community"] as Community;

        using (var context = new RedditCloneDbContext(options!))
        {
            var communityRepository = new CommunityRepository(context);

            communityRepository.Add(community!);

            context.SaveChanges();

            var result = communityRepository.GetCommunityById(community!.Id);

            Assert.Equal(community, result.Value);
        }
    }
    [Fact]
    public void CreateCommunity_ShouldReturnCommunity_WhenCommunityExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var community = arrange["community"] as Community;

        var userCommunities = arrange["userCommunities"] as UserCommunities;

        using (var context = new RedditCloneDbContext(options!))
        {
            var communityRepository = new CommunityRepository(context);

            var userCommunitiesRepository = new UserCommunitiesRepository(context);

            communityRepository.Add(community!);

            userCommunitiesRepository.Add(userCommunities!);

            context.SaveChanges();

            Assert.Equal(1, context.Communities.Count());

            Assert.Equal(1, context.UserCommunities.Count());
        }
    }

    [Fact]
    public void UpdateCommunity_ShouldUpdateCommunity_WhenCommunityExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var community = arrange["community"] as Community;

        var userCommunities = arrange["userCommunities"] as UserCommunities;

        using (var context = new RedditCloneDbContext(options!))
        {
            var communityRepository = new CommunityRepository(context);

            var userCommunitiesRepository = new UserCommunitiesRepository(context);

            communityRepository.Add(community!);

            userCommunitiesRepository.Add(userCommunities!);

            context.SaveChanges();

            communityRepository.UpdateCommunityById(
                community!.Id,
                userCommunities!.UserId,
                arrange["newName"].ToString()!,
                arrange["newDescription"].ToString()!,
                arrange["newTopic"].ToString()!);

            context.SaveChanges();

            Assert.Equal(1, context.Communities.Count());

            Assert.Equal(1, context.UserCommunities.Count());

            Assert.Equal(arrange["newName"].ToString(), context.Communities.First().Name);
        }
    }

    [Fact]
    public void DeleteCommunity_ShouldDeleteCommunity_WhenCommunityExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var community = arrange["community"] as Community;

        var userCommunities = arrange["userCommunities"] as UserCommunities;

        using (var context = new RedditCloneDbContext(options!))
        {
            var communityRepository = new CommunityRepository(context);

            var userCommunitiesRepository = new UserCommunitiesRepository(context);

            communityRepository.Add(community!);

            userCommunitiesRepository.Add(userCommunities!);

            context.SaveChanges();

            Assert.Equal(1, context.Communities.Count());

            communityRepository.DeleteCommunityById(community!.Id, userCommunities!.UserId);

            context.SaveChanges();

            Assert.Equal(0, context.Communities.Count());
        }
    }

    private static Dictionary<string, object> CreateTestArranges()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var name = "TestCommunity";

        var description = "TestDescription";

        var topic = "TestTopic";

        var newName = "TestCommunityNew";

        var newDescription = "TestDescriptionNew";

        var newTopic = "TestTopicNew";

        var userId = Guid.NewGuid();

        var community = Community.Create(
            name,
            description,
            topic,
            new UserId(userId));

        var userCommunities = UserCommunities.Create(
            new UserId(userId),
            community.Id,
            (int)Role.Admin);

        var arrange = new Dictionary<string, object>()
        {
            { "community", community },
            { "userCommunities", userCommunities },
            { "options", options },
            { "name", name },
            { "description", description },
            { "topic", topic },
            { "userId", userId },
            { "newName", newName },
            { "newDescription", newDescription },
            { "newTopic", newTopic }
        };

        return arrange;
    }
}