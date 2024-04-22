namespace RedditClone.Tests.ApplicationTests.CommentVotes.Commands;

using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.CommentVotes.Commands.CreateCommentVote;
using RedditClone.Application.CommentVotes.Results.CreateCommentVoteResult;

public class CreateCommentVoteCommandHandlerTests
{
    [Fact]
    public async Task CreateCommentVoteCommand_ShouldReturnVoteOnCommentResult_WhenCommentIsValid()
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

            var handler = new CreateCommentVoteCommandHandler(commentRepository);

            var command = new CreateCommentVoteCommand(
                comment.Id,
                userId,
                true);

            var result = await handler.Handle(command, default);

            context.SaveChanges();

            Assert.IsType<ErrorOr<CreateCommentVoteResult>>(result);

            Assert.NotEmpty(context.Comments.First().Votes);
        }
    }
}