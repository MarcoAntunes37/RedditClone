namespace RedditClone.API.Endpoints.User.UpdatePassword;

public record UpdatePasswordRequest(
    string OldPassword,
    string NewPassword,
    string MatchPassword
);