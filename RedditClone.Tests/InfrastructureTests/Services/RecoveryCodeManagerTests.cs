using Moq;
using RedditClone.Infrastructure.Services;

namespace RedditClone.Tests.InfrastructureTests.Services;

public class RecoveryCodeManagerTests
{
    [Fact]
    public void GenerateRecoveryCode_ValidInput_Success()
    {
        var email = "hFv0w@example.com";
        var code = "123456";
        var expiration = DateTime.UtcNow.AddMinutes(5);

        var recoveryCodeManagerMock = new Mock<RecoveryCodeManager>();

        recoveryCodeManagerMock.Object.AddCode(email, code, expiration);

        Assert.True(recoveryCodeManagerMock.Object.ValidateCode(email, code));

        recoveryCodeManagerMock.Object.RemoveCode(email);

        Assert.False(recoveryCodeManagerMock.Object.ValidateCode(email, code));
    }
}
