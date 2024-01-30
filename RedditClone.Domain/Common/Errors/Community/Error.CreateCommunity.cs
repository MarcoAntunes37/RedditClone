using ErrorOr;

namespace RedditClone.Domain.Common.Errors.Community;

public static partial class Errors
{
    public static class CreateCommunity
    {
        public static Error EmptyOrNullName => Error.Validation(
            code: "CreateCommunity.EmptyOrNullName",
            description: "Name cannot be empty or null");

        public static Error EmptyOrNullDescription => Error.Validation(
            code: "CreateCommunity.EmptyOrNullDescription",
            description: "Description cannot be empty or null");

        public static Error EmptyOrNullMembersCount => Error.Validation(
            code: "CreateCommunity.EmptyOrNullMembersCount",
            description: "MembersCount cannot be 0 or null");

        public static Error EmptyOrNullTopic => Error.Validation(
            code: "CreateCommunity.EmptyOrNullTopic",
            description: "Topic cannot be empty or null");
    }
}