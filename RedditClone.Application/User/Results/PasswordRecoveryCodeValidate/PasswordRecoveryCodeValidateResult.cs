namespace RedditClone.Application.User.Results.PasswordRecoveryCodeValidate;

public record PasswordRecoveryCodeValidateResult(
    bool IsValid,
    string Email
);