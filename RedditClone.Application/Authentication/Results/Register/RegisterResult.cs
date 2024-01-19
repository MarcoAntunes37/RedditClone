namespace RedditClone.Application.Authentication.Results.Register;

public record RegisterResult(
    string FirstName,
    string LastName,
    string Email,
    string Token
);