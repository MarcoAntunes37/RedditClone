namespace RedditClone.API.Endpoints.User.PasswordRecoveryNewPassword;

public record PasswordRecoveryNewPasswordRequest(
    string Email,
    string NewPassword,
    string ConfirmPassword);