namespace RedditClone.Tests.ApplicationTests.User.Commands;

using Moq;
using ErrorOr;
using RedditClone.Application.Persistence;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.User.Results.UpdatePassword;
using RedditClone.Application.User.Commands.UpdatePassword;

public class UpdatePasswordCommandHandlerTest
{
    [Fact]
    public async void UpdateProfileCommand_ShouldReturnUpdateProfileResult_WhenUserIsValid()
    {
       var userRepositoryMock = new Mock<IUserRepository>();

       var handler = new UpdatePasswordCommandHandler(
           userRepositoryMock.Object);

        var command = new UpdatePasswordCommand(
            new UserId(Guid.NewGuid()),
            "oldPassword",
            "newPassword",
            "matchPassword");

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<UpdatePasswordResult>>(result);

        userRepositoryMock.Verify(r => r.UpdatePasswordById(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}