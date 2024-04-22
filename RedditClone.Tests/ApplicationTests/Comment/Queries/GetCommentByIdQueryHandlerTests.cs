namespace RedditClone.Tests.ApplicationTests.Comment.Queries;

using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.Comment.Queries.GetCommentById;
using RedditClone.Application.Comment.Results.GetCommentByIdResult;

public class GetCommentByIdQueryHandlerTests
{
    [Fact]
    public async void GetCommentByIdQuery_ShouldReturnGetCommentByIdResult_WhenCommentIsValid()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var userId = new UserId(Guid.NewGuid());

        var communityId = new CommunityId(Guid.NewGuid());

        var postId = new PostId(Guid.NewGuid());

        var comment = Comment.Create(
            userId,
            communityId,
            postId,
            "TestContent",
            new List<Votes>(),
            new List<Replies>());

        using (var context = new RedditCloneDbContext(options))
        {
            var commentRepository = new CommentRepository(context);

            context.Comments.Add(comment);

            context.SaveChanges();

            var handler = new GetCommentByIdQueryHandler(commentRepository);

            var query = new GetCommentByIdQuery(comment.Id);

            var result = await handler.Handle(query, default);

            Assert.IsType<GetCommentByIdResult>(result);

            Assert.NotNull(result);
        }
    }
}