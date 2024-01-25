namespace RedditClone.Contracts.Authentication;

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