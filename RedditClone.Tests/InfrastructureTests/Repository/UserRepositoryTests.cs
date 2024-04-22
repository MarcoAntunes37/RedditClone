namespace RedditClone.Tests.InfrastructureTests.Repository;

using Microsoft.EntityFrameworkCore;
using RedditClone.Domain.UserAggregate;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Infrastructure.Persistence.Repositories;

public class UserRepositoryTests
{
    [Fact]
    public void UserRepository_ShouldReturnUserById_WhenUserExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var user = arrange["user"] as User;

        using (var context = new RedditCloneDbContext(options!))
        {
            var userRepository = new UserRepository(context);

            userRepository.Add(user!);

            context.SaveChanges();

            Assert.Equal(1, context.Users.Count());

            var result = userRepository.GetUserById(user!.Id);

            Assert.NotNull(result);
        }
    }

    [Fact]
    public void UserRepository_ShouldReturnUserByEmail_WhenUserExists()
    {
        // Arrange
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var user = arrange["user"] as User;

        using (var context = new RedditCloneDbContext(options!))
        {
            var userRepository = new UserRepository(context);

            userRepository.Add(user!);

            context.SaveChanges();

            Assert.Equal(1, context.Users.Count());

            var result = userRepository.GetUserByEmail(user!.Email);

            Assert.NotNull(result);
        }
    }

    [Fact]
    public void UserRepository_ShouldReturnUserByUsername_WhenUserExists()
    {
        // Arrange
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var user = arrange["user"] as User;

        using (var context = new RedditCloneDbContext(options!))
        {
            var userRepository = new UserRepository(context);

            userRepository.Add(user!);

            context.SaveChanges();

            Assert.Equal(1, context.Users.Count());

            var result = userRepository.GetUserByUsername(user!.Username);

            Assert.NotNull(result);
        }
    }
    [Fact]
    public void UserAdd_ShouldAddUser_WhenUserIsValid()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var user = arrange["user"] as User;

        using (var context = new RedditCloneDbContext(options!))
        {
            var userRepository = new UserRepository(context);

            userRepository.Add(user!);

            context.SaveChanges();

            Assert.Equal(1, context.Users.Count());
        }
    }

    [Fact]
    public void UserRepository_ShouldRemovesUser_WhenUserExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var user = arrange["user"] as User;

        using (var context = new RedditCloneDbContext(options!))
        {
            var userRepository = new UserRepository(context);

            userRepository.Add(user!);

            context.SaveChanges();

            userRepository.DeleteUserById(user!.Id, user.Id);

            context.SaveChanges();

            Assert.Empty(context.Users);
        }
    }
    [Fact]
    public void UserRepository_ShouldUpdateUserProfile_WhenUserExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var user = arrange["user"] as User;

        using (var context = new RedditCloneDbContext(options!))
        {
            var userRepository = new UserRepository(context);

            userRepository.Add(user!);

            context.SaveChanges();

            userRepository.UpdateProfileById(user!.Id, "Johnathan", "Does", "qK9s1V@example.com");

            context.SaveChanges();

            Assert.Equal(1, context.Users.Count());

            Assert.NotEqual(arrange["firstName"], user.Firstname);

            Assert.NotEqual(arrange["lastName"], user.Lastname);

            Assert.NotEqual(arrange["email"], user.Email);
        }
    }
    [Fact]
    public void UserRepository_ShouldUpdateUserPassword_WhenUserExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var user = arrange["user"] as User;

        using (var context = new RedditCloneDbContext(options!))
        {
            var userRepository = new UserRepository(context);

            userRepository.Add(user!);

            context.SaveChanges();

            userRepository.UpdatePasswordById(user!.Id, arrange["password"].ToString()!, "newPassword", "newPassword");

            context.SaveChanges();

            Assert.Equal(1, context.Users.Count());

            Assert.NotEqual(arrange["hashedPassword"], user.Password);
        }
    }

    [Fact]
    public void UserRepository_ShouldRecoveryPassword_WhenRecoveryExists()
    {
        var arrange = CreateTestArranges();

        var options = arrange["options"] as DbContextOptions<RedditCloneDbContext>;

        var user = arrange["user"] as User;

        using (var context = new RedditCloneDbContext(options!))
        {
            var userRepository = new UserRepository(context);

            userRepository.Add(user!);

            context.SaveChanges();

            Assert.Equal(1, context.Users.Count());

            userRepository.UpdateRecoveredPassword(user!.Email, "newPassword", "newPassword");

            context.SaveChanges();

            Assert.NotEqual(arrange["hashedPassword"], user.Password);
        }
    }

    private static Dictionary<string, object> CreateTestArranges()
    {
        var options = new DbContextOptionsBuilder<RedditCloneDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var firstName = "John";
        var lastName = "Doe";
        var username = "johnDoe";
        var password = "password";
        var email = "qK9sV@example.com";

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        var user = User.Create(
            firstName,
            lastName,
            username,
            hashedPassword,
            email);

        var arrange = new Dictionary<string, object>
        {
            { "user", user },
            { "options", options },
            { "hashedPassword", hashedPassword },
            { "firstName", firstName },
            { "lastName", lastName },
            { "username", username },
            { "password", password },
            { "email", email }
        };

        return arrange;
    }
}