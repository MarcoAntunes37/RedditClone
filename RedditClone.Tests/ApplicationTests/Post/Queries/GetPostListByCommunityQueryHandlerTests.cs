namespace RedditClone.Tests.ApplicationTests.Post.Queries;

using Microsoft.EntityFrameworkCore;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.Comment.Queries.GetPostListByCommunityId;
using RedditClone.Application.Community.Queries.GetPostListByCommunityId;
using RedditClone.Application.Post.Results.GetPostListByCommunityIdResult;

public class GetPostListByCommunityQueryHandlerTests
{
    [Fact]
    public async Task GetPostListByCommunityQuery_ShouldReturnGetPostListByCommunityResult_WhenPostIsValid()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using (var context = new RedditCloneDbContext(options))
        {
            var postRepository = new PostRepository(context);

            var handler = new GetPostListByCommunityIdQueryHandler(postRepository);

            var query = new GetPostListByCommunityIdQuery(new CommunityId(Guid.NewGuid()), 1, 20);

            var result = await handler.Handle(query, default);

            Assert.IsType<GetPostListByCommunityIdResult>(result);
        }
    }
}