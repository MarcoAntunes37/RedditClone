using RedditClone.Contracts.Community;

namespace RedditClone.Contracts.Register;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string Password,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<UserCommunities> Communities
);