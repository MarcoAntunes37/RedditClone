namespace RedditClone.Contracts.Authentication;

public record RegisterResponse(
    string FirstName,
    string LastName,
    string Email,
    string Token
);