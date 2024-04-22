namespace RedditClone.Tests.ApplicationTests.Post.Commands;

using Moq;
using ErrorOr;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.Post.Commands.UpdatePost;
using RedditClone.Application.Post.Results.UpdatePostResult;
using RedditClone.Application.Common.Interfaces.Persistence;

public class UpdatePostCommandHandlerTests
{
    [Fact]
    public async Task UpdatePostCommand_ShouldReturnUpdatePostResult_WhenPostIsValid()
    {

        var mockUserCommunitiesRepository = new Mock<IUserCommunitiesRepository>();
        var mockPostRepository = new Mock<IPostRepository>();
        var handler = new UpdatePostCommandHandler(
            mockPostRepository.Object);

        var command = new UpdatePostCommand(
            new PostId(Guid.NewGuid()),
            new UserId(Guid.NewGuid()),
            "TestTitle",
            "TestContent");

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<UpdatePostResult>>(result);

        mockPostRepository.Verify(r => r.UpdatePostById(It.IsAny<PostId>(), It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}