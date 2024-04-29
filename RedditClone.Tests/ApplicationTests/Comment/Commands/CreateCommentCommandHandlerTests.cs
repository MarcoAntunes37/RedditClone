namespace RedditClone.Tests.ApplicationTests.Comment.Commands;

using Moq;
using ErrorOr;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Comment.Commands.CreateComment;
using RedditClone.Application.Comment.Results.CreateCommentResult;

public class CreateCommentCommandHandlerTests
{
    [Fact]
    public async void CreateCommentCommand_ShouldReturnCreateCommentResult_WhenCommentIsValid()
    {
        var commentRepositoryMock = new Mock<ICommentRepository>();

        var userCommunitiesRepositoryMock = new Mock<IUserCommunitiesRepository>();

        var postRepositoryMock = new Mock<IPostRepository>();

        var handler = new CreateCommentCommandHandler(
            commentRepositoryMock.Object,
            postRepositoryMock.Object,
            userCommunitiesRepositoryMock.Object);

        var command = new CreateCommentCommand(
            new UserId(Guid.NewGuid()),
            new CommunityId(Guid.NewGuid()),
            new PostId(Guid.NewGuid()),
            "TestContent",
            new List<Votes>(),
            new List<Replies>());

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<CreateCommentResult>>(result);

        commentRepositoryMock.Verify(r => r.Add(It.IsAny<Comment>()), Times.Once);
    }
}