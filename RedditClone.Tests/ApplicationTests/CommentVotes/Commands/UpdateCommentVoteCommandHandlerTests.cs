namespace RedditClone.Tests.ApplicationTests.CommentVotes.Commands;

using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.CommentVotes.Commands.UpdateCommentVote;
using RedditClone.Application.CommentVotes.Results.UpdateCommentVoteResult;

public class UpdateCommentVoteCommandHandlerTests
{
    [Fact]
    public async Task UpdateCommentVoteCommand_ShouldReturnUpdateVoteOnCommentResult_WhenCommandIsValid()
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

        var vote = Votes.Create(
            comment.Id,
            userId,
            true);

        comment.AddVote(vote);

        using (var context = new RedditCloneDbContext(options))
        {
            var commentRepository = new CommentRepository(context);
            context.Comments.Add(comment);
            context.SaveChanges();
            var handler = new UpdateCommentVoteCommandHandler(commentRepository);
            var command = new UpdateCommentVoteCommand(
                comment.Id,
                vote.Id,
                userId,
                true);
            var result = await handler.Handle(command, default);
            Assert.IsType<ErrorOr<UpdateCommentVoteResult>>(result);
            Assert.NotNull(context.Comments.First().Votes);
        }
    }
}
