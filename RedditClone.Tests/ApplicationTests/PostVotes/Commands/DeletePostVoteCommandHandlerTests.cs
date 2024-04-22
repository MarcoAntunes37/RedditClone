namespace RedditClone.Tests.ApplicationTests.PostVotes.Commands;

using Moq;
using ErrorOr;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.PostVotes.Commands.DeleteVote;

public class DeletePostVoteCommandHandlerTests
{
    [Fact]
    public async Task DeleteVoteCommand_ShouldReturnDeleteVoteResult_WhenPostIsValid()
    {
        // Arrange
        var mockPostRepository = new Mock<IPostRepository>();

        var handler = new DeletePostVoteCommandHandler(
            mockPostRepository.Object);

        var command = new DeletePostVoteCommand(
            new VoteId(Guid.NewGuid()),
            new PostId(Guid.NewGuid()),
            new UserId(Guid.NewGuid()));

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<DeletePostVoteCommand>>(result);

        mockPostRepository.Verify(r => r.DeletePostVoteById(It.IsAny<PostId>(), It.IsAny<VoteId>(), It.IsAny<UserId>()), Times.Once);
    }
}