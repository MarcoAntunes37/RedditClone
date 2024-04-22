namespace RedditClone.Tests.InfrastructureTests.Integrations.UserIntegrationsTests;

using System.Threading.Tasks;
using RedditClone.Application.User.Commands.Register;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class UserTests(IntegrationTestsWebApplicationFactory factory)
: BaseIntegrationTests(factory)
{
    [Fact]
    public async Task CreateUser_ShouldAddUserToDatabase_WhenUserIsValidAsync()
    {
        var command = new RegisterCommand(
            "dev",
            "tester",
            "devtester",
            "password",
            "password",
            "hFv0w@example.com");

        var errorOrUser = await _sender.Send(command);

        var userId = errorOrUser.Value.User.Id.Value;

        var user = _dbContext.Users
            .FirstOrDefault(u => u.Id == new UserId(userId));

        Assert.NotNull(user);
    }
}
