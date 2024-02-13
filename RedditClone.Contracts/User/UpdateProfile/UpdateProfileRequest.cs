namespace RedditClone.Contracts.UpdateProfile;

public record UpdateProfileRequest(
    string Firstname,
    string Lastname,
    string Email
);