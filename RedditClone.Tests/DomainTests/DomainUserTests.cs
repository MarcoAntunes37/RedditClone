namespace RedditClone.Tests;

using RedditClone.Domain.UserAggregate;

public class DomainUserTests
{
    [Fact]
    public void ShouldReturnUserObjectOnCreate()
    {
        string firstname = "John";
        string lastname = "Doe";
        string username = "JohnDoe";
        string password = "JohnDoe@123";
        string email = "JohnDoe@example.com";
        DateTime createdAt = DateTime.Now;
        DateTime updatedAt = DateTime.Now;

        var user = User.Create(
            firstname,
            lastname,
            username,
            password,
            email,
            createdAt,
            updatedAt);

        Assert.NotNull(user);
        Assert.Equal(firstname, user.Firstname);
        Assert.Equal(lastname, user.Lastname);
        Assert.Equal(username, user.Username);
        Assert.Equal(password, user.Password);
        Assert.Equal(email, user.Email);
        Assert.Equal(createdAt, user.CreatedAt);
        Assert.Equal(updatedAt, user.UpdatedAt);
    }

    [Fact]
    public void ShouldReturnUserObjectWithNewDataOnUpdateProfile()
    {
        string firstname = "John";
        string lastname = "Doe";
        string username = "JohnDoe";
        string password = "JohnDoe@123";
        string email = "JohnDoe@example.com";
        DateTime createdAt = DateTime.Now;
        DateTime updatedAt = DateTime.Now;

        string newFirstname = "Johnathan";
        string newLastname = "Does";
        string newEmail = "johnathandoes@example.com";

        DateTime OldUpdatedAt = updatedAt;

        var user = User.Create(
            firstname,
            lastname,
            username,
            password,
            email,
            createdAt,
            updatedAt);

        user.UpdateProfile(
            newFirstname,
            newLastname,
            newEmail
        );

        Assert.NotNull(user);
        Assert.Equal(user.Firstname, newFirstname);
        Assert.Equal(user.Lastname, newLastname);
        Assert.Equal(user.Email, newEmail);
        Assert.True(user.UpdatedAt > OldUpdatedAt);
    }

    [Fact]
    public void ShouldReturnUserObjectWithNewPasswordUpdate()
    {
        string firstname = "John";
        string lastname = "Doe";
        string username = "JohnDoe";
        string password = "JohnDoe@123";
        string email = "JohnDoe@example.com";
        DateTime createdAt = DateTime.Now;
        DateTime updatedAt = DateTime.Now;

        string newPassword = "JohnDoe@1234";

        DateTime OldUpdatedAt = updatedAt;

        var user = User.Create(
            firstname,
            lastname,
            username,
            password,
            email,
            createdAt,
            updatedAt);

        user.UpdatePassword(
            newPassword
        );

        Assert.NotNull(user);
        Assert.Equal(user.Password, newPassword);
        Assert.True(user.UpdatedAt > OldUpdatedAt);
    }
}