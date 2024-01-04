namespace RedditClone.Application.Authentication.Results.Register;

public record RegisterResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);