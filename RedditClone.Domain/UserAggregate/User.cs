namespace RedditClone.Domain.UserAggregate;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class User : Entity
{
    public UserId Id { get; private set; }
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

#pragma warning disable CS8618
    private User() { }
#pragma warning restore

    private User(
        UserId id,
        string firstname,
        string lastname,
        string username,
        string password,
        string email,
        DateTime createdAt,
        DateTime updatedAt

    )
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        Username = username;
        Password = password;
        Email = email;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static User Create(
        string firstname,
        string lastname,
        string username,
        string password,
        string email,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        User user = new(
            new UserId(Guid.NewGuid()),
            firstname,
            lastname,
            username,
            password,
            email,
            createdAt,
            updatedAt
        );
        user.Raise(new UserCreatedDomainEvent(Guid.NewGuid(), user.Id));
        return user;
    }

    public void UpdateProfile(
        string firstname,
        string lastname,
        string email)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePassword(string password)
    {
        Password = password;
        UpdatedAt = DateTime.UtcNow;
    }
}