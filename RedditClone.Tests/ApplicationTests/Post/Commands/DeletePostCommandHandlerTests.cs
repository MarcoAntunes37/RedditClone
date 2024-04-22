namespace RedditClone.Tests.ApplicationTests.Post.Commands;

using Moq;
using ErrorOr;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.Post.Commands.DeletePost;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Post.Results.DeletePostResult;

public class DeletePostCommandHandlerTests
{
    [Fact]
    public async Task DeletePostCommand_ShouldReturnDeletePostResult_WhenPostIsValid()
    {
        var mockUserCommunitiesRepository = new Mock<IUserCommunitiesRepository>();

        var mockPostRepository = new Mock<IPostRepository>();

        var handler = new DeletePostCommandHandler(
            mockPostRepository.Object);

        var command = new DeletePostCommand(
            new PostId(Guid.NewGuid()),
            new UserId(Guid.NewGuid()));

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<DeletePostResult>>(result);

        mockPostRepository.Verify(r => r.DeletePostById(It.IsAny<PostId>(), It.IsAny<UserId>()), Times.Once);
    }
}