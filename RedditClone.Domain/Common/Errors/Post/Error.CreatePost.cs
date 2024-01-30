using ErrorOr;

namespace RedditClone.Domain.Common.Errors.Post;

public static partial class Errors
{
    public static class CreatePost
    {
        public static Error EmptyOrNullTitle => Error.Validation(
            code: "CreatePost.EmptyOrNullTitle",
            description: "Title cannot be empty or null");

        public static Error EmptyOrNullContent => Error.Validation(
            code: "CreatePost.EmptyOrNullContent",
            description: "Content cannot be empty or null");
    }
}