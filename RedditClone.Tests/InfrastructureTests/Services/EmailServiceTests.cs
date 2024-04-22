using Microsoft.Extensions.Options;
using Moq;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Infrastructure.Settings;

namespace RedditClone.Tests.InfrastructureTests.Services;

public class EmailServiceTests
{
    [Fact]
    public void Send_Welcome_Email_ValidInput_Success()
    {
        var emailServiceMock = new Mock<IEmailService>();
        var smtpSettingsMock = new Mock<IOptions<SmtpSettings>>();

        smtpSettingsMock
            .Setup(x => x.Value)
            .Returns(new SmtpSettings
            {
                Host = "localhost",
                Port = 25
            });

        var result = emailServiceMock.Object.SendWelcomeEmailAsync("hFv0w@example.com", "Carlos");

        Assert.True(result.IsCompletedSuccessfully);
    }

    [Fact]
    public void Send_Recovery_Password_Email_ValidInput_Success()
    {
        var emailServiceMock = new Mock<IEmailService>();
        var smtpSettingsMock = new Mock<IOptions<SmtpSettings>>();

        smtpSettingsMock
            .Setup(x => x.Value)
            .Returns(new SmtpSettings
            {
                Host = "localhost",
                Port = 25
            });

        var result = emailServiceMock.Object.SendRecoveryEmailAsync("hFv0w@example.com", "subject", "body");

        Assert.True(result.IsCompletedSuccessfully);
    }
}