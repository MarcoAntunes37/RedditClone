namespace RedditClone.Contracts.Register;

public record RegisterRequest(
    string Firstname,
    string Lastname,
    string Email,
    string Username,
    string Password,
    string RepeatPassword,
    DateTime CreatedAt,
    DateTime UpdatedAt
);