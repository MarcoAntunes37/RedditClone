using ErrorOr;

namespace RedditClone.Domain.Common.Errors.Comment;

public static partial class Errors
{
    public static class CreateComment
    {
        public static Error EmptyOrNullContent => Error.Validation(
            code: "CreateComment.EmptyOrNullContent",
            description: "Content cannot be empty or null");
    }
}