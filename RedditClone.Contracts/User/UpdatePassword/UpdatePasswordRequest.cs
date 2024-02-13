namespace RedditClone.Contracts.UpdatePassword;

public record UpdatePasswordRequest(
    string OldPassword,
    string NewPassword,
    string RepeatNewPassword
);