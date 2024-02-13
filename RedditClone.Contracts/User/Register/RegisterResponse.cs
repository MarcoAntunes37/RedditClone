namespace RedditClone.Contracts.Register;

public record RegisterResponse(
    string Firstname,
    string Lastname,
    string Email,
    string Username,
    string Password,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string Token
);

