using RedditClone.Domain.Common.Models;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.Domain.UserAggregate;

public sealed class UserAggregate
    : AggregateRoot<UserId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

#pragma warning disable CS8618
    private UserAggregate() { }
#pragma warning restore

    private UserAggregate(
        UserId userId,
        string firstName,
        string lastName,
        string username,
        string password,
        string email,
        DateTime createdAt,
        DateTime updatedAt

    ) : base(userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Password = password;
        Email = email;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static UserAggregate Create(
        string firstName,
        string lastName,
        string username,
        string password,
        string email,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new(
            UserId.CreateUnique(),
            firstName,
            lastName,
            username,
            password,
            email,
            createdAt,
            updatedAt
        );
    }
}