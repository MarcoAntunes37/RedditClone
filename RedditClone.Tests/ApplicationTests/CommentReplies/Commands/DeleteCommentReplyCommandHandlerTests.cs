namespace RedditClone.Tests.ApplicationTests.CommentReplies.Commands;

using Moq;
using ErrorOr;
using RedditClone.Application.Persistence;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentReplies.Commands.DeleteCommentReply;
using RedditClone.Application.CommentReplies.Results.DeleteCommentReplyResults;

public class DeleteCommentReplyCommandHandlerTests
{
    [Fact]
    public async void DeleteCommentReply_ShouldReturnDeleteReplyOnCommentResult_WhenCommentIsValid()
    {
        var commentRepositoryMock = new Mock<ICommentRepository>();

        var handler = new DeleteCommentReplyCommandHandler(commentRepositoryMock.Object);

        var command = new DeleteCommentReplyCommand(
            new CommentId(Guid.NewGuid()),
            new ReplyId(Guid.NewGuid()),
            new UserId(Guid.NewGuid()));

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<DeleteCommentReplyResult>>(result);

        commentRepositoryMock.Verify(r => r.DeleteCommentReplyById(It.IsAny<CommentId>(), It.IsAny<ReplyId>(), It.IsAny<UserId>()), Times.Once);
    }
}