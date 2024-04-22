namespace RedditClone.Tests.ApplicationTests.PostVotes.Commands;

using Moq;
using ErrorOr;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.PostVotes.Commands.UpdateVote;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.PostVotes.Results.UpdatePostVoteResult;

public class UpdatePostVoteCommandHandlerTests
{
    [Fact]
    public async Task UpdateVoteCommand_ShouldReturnUpdateVoteResult_WhenPostIsValid()
    {
        // Arrange
        var mockPostRepository = new Mock<IPostRepository>();

        var handler = new UpdatePostVoteCommandHandler(
            mockPostRepository.Object);

        var command = new UpdatePostVoteCommand(
            new VoteId(Guid.NewGuid()),
            new PostId(Guid.NewGuid()),
            new UserId(Guid.NewGuid()),
            true);

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<UpdatePostVoteResult>>(result);

        mockPostRepository.Verify(r => r.UpdatePostVoteById(It.IsAny<PostId>(), It.IsAny<VoteId>(), It.IsAny<UserId>(), It.IsAny<bool>()), Times.Once);
    }
}