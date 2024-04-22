namespace RedditClone.Tests.ApplicationTests.User.Commands;

using Moq;
using ErrorOr;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.PasswordRecoveryNewPassword;
using RedditClone.Application.User.Commands.PasswordRecoveryNewPassword;

public class PasswordRecoveryNewPasswordCommandHandlerTests
{
    [Fact]
    public async void PasswordRecoveryNewPasswordCommand_ShouldReturnPasswordRecoveryNewPasswordResult_WhenUserIsValid()
    {
        var userRepositoryMock = new Mock<IUserRepository>();

        var handler = new PasswordRecoveryNewPasswordCommandHandler(
            userRepositoryMock.Object);

        var command = new PasswordRecoveryNewPasswordCommand(
            "qK9sV@example.com",
            "newPassword",
            "newPassword");

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<PasswordRecoveryNewPasswordResult>>(result);

        userRepositoryMock.Verify(r => r.UpdateRecoveredPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}