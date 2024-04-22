namespace RedditClone.API.Endpoints.User.Register;

public record RegisterResponse(
    string Firstname,
    string Lastname,
    string Email,
    string Username,
    string Password,
    string RepeatPassword,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
