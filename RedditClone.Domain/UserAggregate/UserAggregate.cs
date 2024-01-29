using RedditClone.Domain.Common.Models;
using RedditClone.Domain.UserAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.Domain.UserAggregate;

public sealed class UserAggregate :
AggregateRoot<UserId>
{
    private readonly List<UserCommunities> _communities = new();
    public string FirstName { get; }
    public string LastName { get; }
    public string Username { get; }
    public string Password { get; }
    public string Email { get; }
    public IReadOnlyList<UserCommunities> Communities => _communities.ToList();
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

    private UserAggregate(
        UserId userId,
        string firstName,
        string lastName,
        string username,
        string password,
        string email,
        DateTime createdAt,
        DateTime updatedAt,
        List<UserCommunities> communities

    ) : base(userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Password = password;
        Email = email;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _communities = communities ?? new List<UserCommunities>();
    }

    public static UserAggregate Create(
        string firstName,
        string lastName,
        string username,
        string password,
        string email,
        DateTime createdAt,
        DateTime updatedAt,
        List<UserCommunities> communities
    ){
        return new(
            UserId.CreateUnique(),
            firstName,
            lastName,
            username,
            password,
            email,
            createdAt,
            updatedAt,
            communities
        );
    }
}