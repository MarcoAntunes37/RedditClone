namespace RedditClone.Tests.ApplicationTests.PostVotes.Commands;

using Moq;
using ErrorOr;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.PostVotes.Commands.CreateVote;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Post.Results.CreatePostVoteResult;

public class CreatePostVoteCommandHandlerTests
{
    [Fact]
    public async Task CreatePostVoteCommand_ShouldReturnCreateVoteResult_WhenPostIsValid()
    {
        // Arrange
        var mockPostRepository = new Mock<IPostRepository>();

        var handler = new CreatePostVoteCommandHandler(
            mockPostRepository.Object);

        var command = new CreatePostVoteCommand(
            new PostId(Guid.NewGuid()),
            new UserId(Guid.NewGuid()),
            true);

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<CreatePostVoteResult>>(result);

        mockPostRepository.Verify(r => r.AddPostVote(It.IsAny<PostId>(), It.IsAny<UserId>(), It.IsAny<bool>()), Times.Once);
    }
}