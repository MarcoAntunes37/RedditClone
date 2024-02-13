namespace RedditClone.Contracts.PasswordRecoveryNewPassword;

public record PasswordRecoveryNewPasswordRequest(
    string NewPassword,
    string RepeatPassword,
    string Email
);