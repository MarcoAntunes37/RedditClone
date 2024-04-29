namespace RedditClone.Domain.Common.Errors;

using ErrorOr;

public static class Errors
{
    public class User
    {
        public static Error UsernameAlreadyExists => Error.Conflict(
            code: "User.UsernameAlreadyExists", description: "Username already exists");
        public static Error EmailAlreadyExists => Error.Conflict(
            code: "User.EmailAlreadyExists", description: "Email already exists");
        public static Error UserNotFound => Error.NotFound(
            code: "User.NotFound", description: "User not found");
        public static Error InvalidCredentials => Error.Unauthorized(
            code: "User.InvalidCredentials", description: "Invalid credentials");
        public static Error PasswordsDoNotMatch => Error.Failure(
            code: "User.PasswordsDoNotMatch", description: "Passwords do not match");
        public static Error OnlyDeleteSelf => Error.Forbidden(
            code: "User.OnlyDeleteSelf", description: "Cant delete other users");
    }

    public class Community
    {
        public static Error CommunityNotFound => Error.NotFound(
            code: "Community.NotFound", description: "Community not found");
        public static Error CommunityNameAlreadyExists => Error.Conflict(
            code: "Community.NameAlreadyExists", description: "Community name already exists");
    }

    public class UserCommunities
    {
        public static Error UserAlreadyInCommunity => Error.Conflict(
            code: "UserCommunities.AlreadyInCommunity", description: "User already in community");
        public static Error UserNotInCommunity => Error.Unauthorized(
            code: "UserCommunities.NotInCommunity", description: "User not in community");
        public static Error UserIsNotCommunityAdmin => Error.Unauthorized(
            code: "UserCommunities.UserIsNotCommunityAdmin", description: "User is not community admin");
        public static Error AdminCantLeaveCommunity => Error.Forbidden(
            code: "UserCommunities.AdminCantLeaveCommunity", description: "Admin cant leave community");
    }

    public class Posts
    {
        public static Error PostNotFound => Error.NotFound(
            code: "Post.NotFound", description: "Post not found");
        public static Error VoteNotFound => Error.NotFound(
            code: "Post.VoteNotFound", description: "Vote not found");
        public static Error PostNotOwnedByUser => Error.Forbidden(
            code: "Post.PostNotOwnedByUser", description: "Post not owned by user");
    }


    public class PostVotes
    {
        public static Error VoteNotFound => Error.NotFound(
            code: "PostVotes.NotFound", description: "Vote not found");
        public static Error UserAlreadyVoted => Error.Conflict(
            code: "PostVotes.UserAlreadyVoted", description: "User already voted");
        public static Error UserNotVoteOwner => Error.Conflict(
            code: "PostVotes.UserNotVoteOwner", description: "User not vote owner");

    }
    public class Comments
    {
        public static Error CommentNotFound => Error.NotFound(
            code: "Comment.NotFound", description: "Comment not found");
        public static Error CommentNotOwnerByUser => Error.Forbidden(
            code: "Comment.CommentNotOwnerByUser", description: "Comment not owned by user");
    }

    public class CommentVotes
    {
        public static Error VoteNotFound => Error.NotFound(
            code: "CommentVotes.VoteNotFound", description: "Vote not found");
        public static Error UserNotVoteOwner => Error.Forbidden(
            code: "CommentVotes.UserNotVoteOwner", description: "User not vote owner");
        public static Error UserAlreadyVoted => Error.Conflict(
            code: "CommentVotes.UserAlreadyVoted", description: "User already voted");
    }

    public class CommentReplies
    {
        public static Error ReplyNotFound => Error.NotFound(
            code: "CommentReplies.ReplyNotFound", description: "Reply not found");
        public static Error ReplyNotOwnerByUser => Error.Forbidden(
            code: "CommentReplies.ReplyNotOwnerByUser", description: "Reply not owned by user");
    }

    public class ReplyVotes
    {
        public static Error VoteNotFound => Error.NotFound(
            code: "ReplyVotes.VoteNotFound", description: "Vote not found");
        public static Error UserNotVoteOwner => Error.Forbidden(
            code: "ReplyVotes.UserNotVoteOwner", description: "User not vote owner");
    }
}