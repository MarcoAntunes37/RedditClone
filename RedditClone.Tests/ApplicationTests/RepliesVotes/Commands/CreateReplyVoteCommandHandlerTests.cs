namespace RedditClone.Tests.ApplicationTests.RepliesVotes.Commands;

using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.ReplyVotes.Commands.CreateReplyVote;
using RedditClone.Application.ReplyVotes.Results.CreateReplyVoteResult;

public class CreateReplyVoteCommandHandlerTests
{
    [Fact]
    public async Task CreateReplyVoteCommand_ShouldReturnVoteOnReplyResult_WhenVoteIsValid()
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

        using (var context = new RedditCloneDbContext(options))
        {
            var commentRepository = new CommentRepository(context);

            context.Comments.Add(comment);

            context.SaveChanges();

            var handler = new CreateReplyVoteCommandHandler(commentRepository);

            var command = new CreateReplyVoteCommand(
                new CommentId(Guid.NewGuid()),
                new ReplyId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()),
                true);

            var result = await handler.Handle(command, default);

            Assert.IsType<ErrorOr<CreateReplyVoteResult>>(result);
        }
    }
}