namespace RedditClone.Tests.ApplicationTests.User.Commands;

using Moq;
using ErrorOr;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results;
using RedditClone.Application.User.Commands.Delete;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class DeleteCommandHandlerTest
{
    [Fact]
    public async void DeleteCommand_ShouldReturnDeleteResult_WhenUserIsValid()
    {
       var userRepositoryMock = new Mock<IUserRepository>();

       var handler = new DeleteUserCommandHandler(
           userRepositoryMock.Object);

        var userId = new UserId(Guid.NewGuid());

        var command = new DeleteUserCommand(userId, userId);

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<DeleteResult>>(result);

        userRepositoryMock.Verify(r => r.DeleteUserById(It.IsAny<UserId>(), It.IsAny<UserId>()), Times.Once);
    }
}