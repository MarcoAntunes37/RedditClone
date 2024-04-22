namespace RedditClone.Tests.DomainTests;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserAggregate.DomainEvents;

public class DomainUserTests
{
    [Fact]
    public static void Create_User_ValidInput_Success()
    {
        var arrange = CreateTestArranges();

        var user = arrange["user"] as User;

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)user!.GetDomainEvents();

        Assert.NotNull(user);

        Assert.IsType<UserCreatedDomainEvent>(domainEvents.LastOrDefault());

        Assert.Equal(arrange["firstName"], user.Firstname);

        Assert.Equal(arrange["lastName"], user.Lastname);

        Assert.Equal(arrange["username"], user.Username);

        Assert.Equal(arrange["password"], user.Password);

        Assert.Equal(arrange["email"], user.Email);

        user.ClearDomainEvents();
    }

    [Fact]
    public static void UpdateProfile_User_ValidInput_Success()
    {
        var arrange = CreateTestArranges();

        var user = arrange["user"] as User;

        user!.UpdateProfile(
            arrange["newFirstname"].ToString()!, arrange["newLastname"].ToString()!, arrange["newEmail"].ToString()!);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)user.GetDomainEvents();

        Assert.NotNull(user);

        Assert.IsType<UserProfileUpdatedDomainEvent>(domainEvents.LastOrDefault());

        Assert.Equal(user.Firstname, arrange["newFirstname"].ToString());

        Assert.Equal(user.Lastname, arrange["newLastname"].ToString());

        Assert.Equal(user.Email, arrange["newEmail"].ToString());

        user.ClearDomainEvents();
    }

    [Fact]
    public static void UpdatePassword_User_ValidInput_Success()
    {
        var arrange = CreateTestArranges();

        var user = arrange["user"] as User;

        user!.UpdatePassword(arrange["newPassword"].ToString()!);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)user.GetDomainEvents();

        Assert.NotNull(user);

        Assert.IsType<UserPasswordUpdatedDomainEvent>(domainEvents.LastOrDefault());

        Assert.Equal(user.Password, arrange["newPassword"].ToString());
    }

    [Fact]
    public static void Delete_User_ValidInput_Success()
    {
        var arrange = CreateTestArranges();

        var user = arrange["user"] as User;

        user!.DeleteUser();

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)user!.GetDomainEvents();

        Assert.NotNull(user);

        Assert.IsType<UserDeletedDomainEvent>(domainEvents.LastOrDefault());

        user.ClearDomainEvents();
    }

    public static Dictionary<string, object> CreateTestArranges()
    {
        string firstname = "John";
        string lastname = "Doe";
        string username = "JohnDoe";
        string password = "JohnDoe@123";
        string email = "JohnDoe@example.com";

        string newFirstname = "Johnathan";
        string newLastname = "Does";
        string newEmail = "johnathandoes@example.com";
        string newPassword = "JohnDoe@1234";

        var user = User.Create(
            firstname,
            lastname,
            username,
            password,
            email);

        var arrange = new Dictionary<string, object>()
        {
            { "user", user },
            { "firstName", firstname },
            { "lastName", lastname },
            { "username", username },
            { "password", password },
            { "email", email },
            { "newFirstname", newFirstname },
            { "newLastname", newLastname },
            { "newEmail", newEmail },
            { "newPassword", newPassword }
        };

        return arrange;
    }
}