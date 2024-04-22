namespace RedditClone.Tests.ApplicationTests.Comment.Commands;

using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.Comment.Commands.DeleteComment;
using RedditClone.Application.Comment.Results.DeleteCommentResult;

public class DeleteCommentCommandHandlerTests
{
    [Fact]
    public async void DeleteCommentCommand_ShouldReturnDeleteCommentResult_WhenCommentIsValid()
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

            context.Add(comment);

            context.SaveChanges();

            var handler = new DeleteCommentCommandHandler(commentRepository);

            var command = new DeleteCommentCommand(
                comment.Id,
                userId);

            var result = await handler.Handle(command, default);

            await context.SaveChangesAsync();

            Assert.IsType<ErrorOr<DeleteCommentResult>>(result);

            Assert.Equal(0, context.Comments.Count());
        }
    }
}