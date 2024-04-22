namespace RedditClone.Tests.ApplicationTests.CommentReplies.Commands;

using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.CommentReplies.Commands.CreateCommentReply;
using RedditClone.Application.CommentReplies.Results.CreateCommentReplyResults;

public class CreateCommentReplyCommandHandlerTests
{
    [Fact]
    public async Task CreateCommentReply_ShouldReturnReplyOnCommentResult_WhenCommentIsValid()
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

        var userCommunity = UserCommunities.Create(
            userId,
            communityId,
            Role.Admin);

        using (var context = new RedditCloneDbContext(options))
        {
            var commentRepository = new CommentRepository(context);

            context.Comments.Add(comment);

            context.UserCommunities.Add(userCommunity);

            context.SaveChanges();

            var userCommunitiesRepository = new UserCommunitiesRepository(context);

            var handler = new CreateCommentReplyCommandHandler(commentRepository, userCommunitiesRepository);

            var command = new CreateCommentReplyCommand(
                userId,
                communityId,
                comment.Id,
                "TestContent");

            var result = await handler.Handle(command, default);

            Assert.IsType<ErrorOr<CreateCommentReplyResult>>(result);
        }
    }
}