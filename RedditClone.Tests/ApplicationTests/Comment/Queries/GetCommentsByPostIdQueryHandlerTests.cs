namespace RedditClone.Tests.ApplicationTests.Comment.Queries;

using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.Comment.Queries.GetCommentsByPostId;
using RedditClone.Application.Comment.Queries.GetCommentsListByPostId;
using RedditClone.Application.Comment.Results.GetCommentsListByPostIdResults;

public class GetCommentsByPostIdQueryHandlerTests
{
    [Fact]
    public async void GetCommentsByPostIdQuery_ShouldReturnGetCommentsByPostIdResult_WhenCommentIsValid()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var postId = new PostId(Guid.NewGuid());

        var userId = new UserId(Guid.NewGuid());

        var communityId = new CommunityId(Guid.NewGuid());

        var comment = Comment.Create(
            userId,
            communityId,
            postId,
            "TestContent",
            new List<Votes>(),
            new List<Replies>()
        );

        using (var context = new RedditCloneDbContext(options))
        {
            var commentRepository = new CommentRepository(context);

            context.Comments.Add(comment);

            context.SaveChanges();

            var handler = new GetCommentsByPostIdQueryHandler(commentRepository);

            int page = 1;

            int pageSize = 20;

            var query = new GetCommentsListByPostIdQuery(postId, page, pageSize);

            var result = await handler.Handle(query, default);

            Assert.IsType<GetCommentsListByPostIdResult>(result);

            Assert.Single(result.Comments);
        }
    }
}