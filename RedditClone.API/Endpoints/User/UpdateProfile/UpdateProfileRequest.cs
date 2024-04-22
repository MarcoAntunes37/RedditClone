namespace RedditClone.API.Endpoints.User.UpdateProfile;

public record UpdateProfileRequest(
    string Firstname,
    string Lastname,
    string Email
);