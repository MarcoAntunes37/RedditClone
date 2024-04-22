namespace RedditClone.API.Endpoints.User.Register;

public record RegisterRequest(
    string Firstname,
    string Lastname,
    string Email,
    string Username,
    string Password,
    string RepeatPassword
);
