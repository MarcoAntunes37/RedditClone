namespace RedditClone.Contracts.PasswordRecoveryCodeValidate;

public record PasswordRecoveryCodeValidateRequest(
    string Code,
    string Email
);