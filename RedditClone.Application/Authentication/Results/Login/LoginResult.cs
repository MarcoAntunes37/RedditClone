namespace RedditClone.Application.Authentication.Results.Login;

public record LoginResult(
    string Email,
    string Token
);