using RedditClone.Contracts.Community;

namespace RedditClone.Contracts.Register;

public record RegisterResponse(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string Password,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string Token
);

