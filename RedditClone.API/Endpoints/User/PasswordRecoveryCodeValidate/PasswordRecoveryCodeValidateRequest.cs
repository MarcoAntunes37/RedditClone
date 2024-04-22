namespace RedditClone.API.Endpoints.User.PasswordRecoveryCodeValidate;

public record PasswordRecoveryCodeValidateRequest(
    string Email,
    string Code
);