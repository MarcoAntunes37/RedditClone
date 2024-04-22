namespace RedditClone.Tests.InfrastructureTests.Services;

using Microsoft.Extensions.Options;
using Moq;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Infrastructure.Authentication;
using RedditClone.Infrastructure.Settings;

public class AuthenticationServiceTests
{

    [Fact]
    public void GenerateToken_User_ValidInput_Success()
    {
        Guid userId = Guid.NewGuid();
        string firstname = "John";
        string lastname = "Doe";
        string secret = Guid.NewGuid().ToString();
        string issuer = "TestIssuer";
        string audience = "TestAudience";
        int expires = 60;

        var jwtSettingsMock = new Mock<IOptions<JwtSettings>>();

        jwtSettingsMock
            .Setup(x => x.Value)
            .Returns(new JwtSettings
            {
                Secret = secret,
                Issuer = issuer,
                Audience = audience,
                ExpirationTimeInMinutes = expires
            });

        var dateTimeProviderMock = new Mock<IDateTimeProvider>();

        JwtTokenGenerator jwtTokenGeneratorMock = new(
            dateTimeProviderMock.Object,
            jwtSettingsMock.Object);

        string token = jwtTokenGeneratorMock.GenerateToken(userId, firstname, lastname);

        Assert.NotEmpty(token);
        Assert.NotNull(token);
        Assert.True(token.Length > 10);
    }
}