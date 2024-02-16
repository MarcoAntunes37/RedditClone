namespace RedditClone.Domain.UserAggregate;

using RedditClone.Domain.Common.Models;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class User
    : AggregateRoot<UserId, Guid>
{
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
        UserId userId,
        string firstname,
        string lastname,
        string username,
        string password,
        string email,
        DateTime createdAt,
        DateTime updatedAt

    ) : base(userId)
    {
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
        return new(
            UserId.CreateUnique(),
            firstname,
            lastname,
            username,
            password,
            email,
            createdAt,
            updatedAt
        );
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