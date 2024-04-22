namespace RedditClone.Tests.ApplicationTests.User.Commands;

using Moq;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.UserAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Application.User.Commands.SendPasswordRecoveryEmail;
using RedditClone.Application.User.Results.SendPasswordRecoveryEmail;
using RedditClone.Infrastructure.Persistence.Repositories;

public class SendPasswordRecoveryEmailCommandHandlerTest
{
    [Fact]
    public async void SendPasswordRecoveryEmailCommand_ShouldReturnSendPasswordRecoveryEmailResult_WhenUserIsValid()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

        var emailServiceMock = new Mock<IEmailService>();

        var recoveryCodeManagerMock = new Mock<IRecoveryCodeManager>();

        var user = User.Create(
            "John",
            "Doe",
            "johnDoe",
            "password",
            "qK9sV@example.com");

        using (var context = new RedditCloneDbContext(options))
        {
            var userRepository = new UserRepository(context);

            userRepository.Add(user);

            context.SaveChanges();

            var count = context.Users.Count();

            var handler = new SendPasswordRecoveryEmailCommandHandler(
                emailServiceMock.Object,
                recoveryCodeManagerMock.Object,
                userRepository);

            var command = new SendPasswordRecoveryEmailCommand("qK9sV@example.com");

            var result = await handler.Handle(command, default);

            Assert.IsType<ErrorOr<SendPasswordRecoveryEmailResult>>(result);

            emailServiceMock.Verify(s => s.SendRecoveryEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), default), Times.Once);
        }
    }
}