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
using RedditClone.Application.CommentVotes.Commands.DeleteCommentVote;
using RedditClone.Application.CommentVotes.Results.DeleteCommentVoteResult;

public class DeleteCommentVoteCommandHandlerTests
{
    [Fact]
    public async void DeleteCommentVoteCommand_ShouldReturnDeleteVoteOnCommentResult_WhenCommentIsValid()
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

            var vote = Votes.Create(
                comment.Id,
                userId,
                true);

        using (var context = new RedditCloneDbContext(options))
        {
            var commentRepository = new CommentRepository(context);

            context.Comments.Add(comment);

            await context.SaveChangesAsync();

            var commentVote = context.Comments.First();

            commentVote.AddVote(vote);

            await context.SaveChangesAsync();

            var handler = new DeleteCommentVoteCommandHandler(commentRepository);

            var command = new DeleteCommentVoteCommand(
                comment.Id,
                vote.Id,
                userId);

            var result = await handler.Handle(command, default);

            await context.SaveChangesAsync();

            Assert.IsType<ErrorOr<DeleteCommentVoteResult>>(result);

            Assert.Empty(context.Comments.First().Votes);
        }
    }
}