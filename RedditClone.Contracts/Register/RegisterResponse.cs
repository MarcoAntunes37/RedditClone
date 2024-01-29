namespace RedditClone.Contracts.Register;

public record RegisterResponse(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string Password,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<UserCommunities> Communities,
    string Token
);

public record UserCommunities(
    string CommunityId,
    string Name
);