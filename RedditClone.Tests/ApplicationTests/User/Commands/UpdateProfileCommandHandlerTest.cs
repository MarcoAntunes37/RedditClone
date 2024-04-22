namespace RedditClone.Tests.ApplicationTests.User.Commands;

using Moq;
using ErrorOr;
using RedditClone.Application.Persistence;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.User.Commands.UpdateProfile;
using RedditClone.Application.User.Results.UpdateProfile;

public class UpdateProfileCommandHandlerTest
{
    [Fact]
    public async void UpdateProfileCommand_ShouldReturnUpdateProfileResult_WhenUserIsValid()
    {
       var userRepositoryMock = new Mock<IUserRepository>();

       var handler = new UpdateProfileCommandHandler(
           userRepositoryMock.Object);

        var command = new UpdateProfileCommand(
            new UserId(Guid.NewGuid()),
            "tester",
            "devtester",
            "hFv0w@example.com");

        var result = await handler.Handle(command, default);

        Assert.IsType<ErrorOr<UpdateProfileResult>>(result);

        userRepositoryMock.Verify(r => r.UpdateProfileById(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}