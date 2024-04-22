namespace RedditClone.Tests.ApplicationTests.Comment.Commands;

using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.Comment.Commands.UpdateComment;
using RedditClone.Application.Comment.Results.UpdateCommentResult;

public class UpdateCommentCommandHandlerTests
{
    [Fact]
    public async void UpdateCommentCommand_ShouldReturnUpdateCommentResult_WhenCommentIsValid()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var communityId = new CommunityId(Guid.NewGuid());

        var userId = new UserId(Guid.NewGuid());

        var postId = new PostId(Guid.NewGuid());

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

            var handler = new UpdateCommentCommandHandler(commentRepository);

            var command = new UpdateCommentCommand(
                comment.Id,
                userId,
                "TestContent");

            var result = await handler.Handle(command, default);

            context.SaveChanges();

            Assert.IsType<ErrorOr<UpdateCommentResult>>(result);

            Assert.Single(context.Comments);
        }
    }
}