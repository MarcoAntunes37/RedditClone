using ErrorOr;
using Moq;
using RedditClone.Application.Common.Interfaces.Authentication;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Queries.Login;
using RedditClone.Application.User.Results.Login;

namespace RedditClone.Tests.ApplicationTests.User.Queries;

public class LoginQueryHandlerTest
{
    [Fact]
    public async void LoginQuery_ShouldReturnLoginResult_WhenUserIsValid()
    {
        var userRepositoryMock = new Mock<IUserRepository>();

        var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

        var handler = new LoginQueryHandler(
            jwtTokenGeneratorMock.Object,
            userRepositoryMock.Object);

        var query = new LoginQuery("qK9sV@example.com", "password");

        var result = await handler.Handle(query, default);

        Assert.IsType<ErrorOr<LoginResult>>(result);

        userRepositoryMock.Verify(r => r.GetUserByEmail(It.IsAny<string>()), Times.Once);
    }
}