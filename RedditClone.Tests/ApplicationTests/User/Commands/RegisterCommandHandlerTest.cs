namespace RedditClone.Tests.ApplicationTests.User.Commands;

using Moq;
using ErrorOr;
using RedditClone.Domain.UserAggregate;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.Register;
using RedditClone.Application.User.Commands.Register;
using RedditClone.Application.Common.Interfaces.Authentication;

public class RegisterCommandHandlerTest
{
    [Fact]
    public async void RegisterCommand_ShouldReturnRegisterResult_WhenUserIsValid()
    {
        var userRepositoryMock = new Mock<IUserRepository>();

        var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

        var handler = new RegisterCommandHandler(
            userRepositoryMock.Object,
            jwtTokenGeneratorMock.Object);

        var command = new RegisterCommand(
            "dev",
            "tester",
            "devtester",
            "password",
            "password",
            "hFv0w@example.com");

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<RegisterResult>>(result);

        userRepositoryMock.Verify(r => r.Add(It.IsAny<User>()), Times.Once);

        jwtTokenGeneratorMock.Verify(r => r.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}