namespace RedditClone.Tests.ApplicationTests.RepliesVotes.Commands;

using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.ReplyVotes.Commands.DeleteReplyVote;
using RedditClone.Application.ReplyVotes.Results.DeleteReplyVoteResult;

public class DeleteVoteOnReplyCommandHandlerTests
{
    [Fact]
    public async void DeleteVoteOnReplyCommand_ShouldReturnDeleteVoteOnReplyResult_WhenCommentIsValid()
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

        var reply = Replies.Create(
            userId,
            communityId,
            comment.Id,
            "ContentTest",
            new List<RepliesVotes>());

        var vote = RepliesVotes.Create(
            reply.Id,
            userId,
            true);

        using (var context = new RedditCloneDbContext(options))
        {
            var commentRepository = new CommentRepository(context);

            context.Add(comment);

            await context.SaveChangesAsync();

            var replyComment = context.Comments.First();

            replyComment.AddReply(reply);

            await context.SaveChangesAsync();

            var voteReply = context.Comments.First().Replies.First();

            voteReply.AddReplyVote(vote);

            await context.SaveChangesAsync();

            var handler = new DeleteReplyVoteCommandHandler(commentRepository);

            var command = new DeleteReplyVoteCommand(
                comment.Id,
                reply.Id,
                vote.Id,
                userId);

            var result = await handler.Handle(command, default);

            await context.SaveChangesAsync();

            var votes = context.Comments.First().Replies.First().Votes.Count();

            Assert.IsType<ErrorOr<DeleteReplyVoteResult>>(result);

            Assert.Equal(0, votes);
        }
    }
}