namespace RedditClone.Tests.ApplicationTests.User.Commands;

using Moq;
using ErrorOr;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Application.User.Commands.PasswordRecoveryCodeValidate;
using RedditClone.Application.User.Results.PasswordRecoveryCodeValidate;

public class PasswordRecoveryCodeValidatorCommandHandlerTests
{
    [Fact]
    public async void PasswordRecoveryCodeValidatorCommand_ShouldReturnPasswordRecoveryCodeValidatorResult_WhenUserIsValid()
    {
       var recoveryCodeManager = new Mock<IRecoveryCodeManager>();

       var handler = new PasswordRecoveryCodeValidateCommandHandler(
        recoveryCodeManager.Object);

        var command = new PasswordRecoveryCodeValidateCommand(
            "qK9sV@example.com",
            "123456");

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<PasswordRecoveryCodeValidateResult>>(result);

        recoveryCodeManager.Verify(s => s.ValidateCode(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}
