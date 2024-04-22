namespace RedditClone.Tests.ApplicationTests.Community.Queries;

using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.Comment.Queries.GetCommunitiesList;
using RedditClone.Application.Community.Queries.GetCommunitiesList;
using RedditClone.Application.Community.Results.GetCommunitiesListResult;

public class GetCommunitiesListQueryHandlerTest
{
    [Fact]
    public async void GetCommunitiesListQuery_ShouldReturnGetCommunitiesListResult_WhenCommunityIsValid()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

        var community = Community.Create(
            "Dev",
            "Test",
            "Test",
            new UserId(Guid.NewGuid()));

        using (var context = new RedditCloneDbContext(options))
        {
            var communityRepository = new CommunityRepository(context);

            context.Add(community);

            context.SaveChanges();

            var handler = new GetCommunitiesListQueryHandler(communityRepository);

            var query = new GetCommunitiesListQuery(
                "Dev",
                "Test",
                1,
                10);

            var result = await handler.Handle(query, default);

            Assert.IsType<GetCommunitiesListResult>(result);

            var registerCounter = communityRepository.GetCommunitiesList().Count;

            Assert.Equal(1, registerCounter);
        }
    }
}