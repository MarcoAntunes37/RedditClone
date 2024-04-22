namespace RedditClone.Domain.Common.Errors;

using ErrorOr;

public static class Errors
{
    public class User
    {
        public static Error UsernameAlreadyExists => Error.Conflict(code: "User.UsernameAlreadyExists", description: "Username already exists");
        public static Error EmailAlreadyExists => Error.Conflict(code: "User.EmailAlreadyExists", description: "Email already exists");
        public static Error UserNotFound => Error.NotFound(code: "User.NotFound", description: "User not found");
        public static Error InvalidCredentials => Error.Validation(code: "User.InvalidCredentials", description: "Invalid credentials");
        public static Error PasswordsDoNotMatch => Error.Validation(code: "User.PasswordsDoNotMatch", description: "Passwords do not match");
        public static Error OnlyDeleteSelf => Error.Validation(code: "User.OnlyDeleteSelf", description: "Can not delete other users");
    }

    public class Community
    {
        public static Error CommunityNotFound => Error.NotFound(code: "Community.NotFound", description: "Community not found");
        public static Error CommunityNameAlreadyExists => Error.Conflict(code: "Community.NameAlreadyExists", description: "Community name already exists");
    }

    public class UserCommunities
    {
        public static Error UserAlreadyInCommunity => Error.Conflict(code: "UserCommunities.AlreadyInCommunity", description: "User already in community");
        public static Error UserNotInCommunity => Error.Conflict(code: "UserCommunities.NotInCommunity", description: "User not in community");
        public static Error UserIsNotCommunityAdmin => Error.Conflict(code: "UserCommunities.UserIsNotCommunityAdmin", description: "User is not community admin");
    }

    public class Posts
    {
        public static Error PostNotFound => Error.NotFound(code: "Post.NotFound", description: "Post not found");
        public static Error VoteNotFound => Error.NotFound(code: "Post.VoteNotFound", description: "Vote not found");
    }

    public class Comments
    {
        public static Error CommentNotFound => Error.NotFound(code: "Comment.NotFound", description: "Comment not found");
        public static Error VoteNotFound => Error.NotFound(code: "Comment.VoteNotFound", description: "Vote not found");
        public static Error ReplyNotFound => Error.NotFound(code: "Comment.ReplyNotFound", description: "Reply not found");
    }
}