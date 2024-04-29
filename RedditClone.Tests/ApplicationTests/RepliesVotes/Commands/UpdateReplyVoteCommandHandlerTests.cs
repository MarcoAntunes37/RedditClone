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
using RedditClone.Application.ReplyVotes.Commands.UpdateReplyVote;
using RedditClone.Application.ReplyVotes.Results.UpdateReplyVoteResult;

public class UpdateReplyVoteCommandHandlerTests
{
    [Fact]
    public async Task UpdateVoteOnReplyCommand_ShouldReturnUpdateVoteOnReplyResult_WhenCommandIsValid()
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

        var vote = RepliesVotes.Create(
            reply.Id,
            userId,
            true);

        reply.AddReplyVote(vote);

        comment.AddReply(reply);

        using (var context = new RedditCloneDbContext(options))
        {
            var commentRepository = new CommentRepository(context);

            context.Comments.Add(comment);

            context.SaveChanges();

            var handler = new UpdateReplyVoteCommandHandler(commentRepository);

            var command = new UpdateReplyVoteCommand(
                comment.Id,
                reply.Id,
                vote.Id,
                userId,
                true);

            var result = await handler.Handle(command, default);

            Assert.IsType<ErrorOr<UpdateReplyVoteResult>>(result);

            Assert.NotEmpty(context.Comments.First().Replies.First().Votes);
        }
    }
}
