namespace RedditClone.Tests.ApplicationTests.CommentReplies.Commands;

using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.CommentReplies.Commands.UpdateCommentReply;
using RedditClone.Application.CommentReplies.Results.UpdateCommentReplyResults;

public class UpdateCommentReplyCommandHandlerTests
{
    [Fact]
    public async Task UpdateCommentReplyCommand_ShouldReturnUpdateReplyOnCommentResult_WhenCommandIsValid()
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
            new List<Replies>());

        var reply = Replies.Create(
            userId,
            comment.Id,
            "ContentTest",
            new List<RepliesVotes>());

        comment.AddReply(reply);

        using (var context = new RedditCloneDbContext(options))
        {
            var commentRepository = new CommentRepository(context);

            context.Comments.Add(comment);

            context.SaveChanges();

            var handler = new UpdateCommentReplyCommandHandler(commentRepository);

            var command = new UpdateCommentReplyCommand(
                comment.Id,
                reply.Id,
                userId,
                "TestContent");

            var result = await handler.Handle(command, default);

            Assert.IsType<ErrorOr<UpdateCommentReplyResult>>(result);

            Assert.NotEmpty(context.Comments.First().Replies);
        }
    }
}
