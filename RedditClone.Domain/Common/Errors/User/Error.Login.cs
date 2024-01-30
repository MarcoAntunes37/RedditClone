using ErrorOr;

namespace RedditClone.Domain.Common.Errors.User;

public static partial class Errors
{
    public static class Login
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Login.InvalidCredentials",
            description: "Invalid Credentials");

        public static Error EmptyOrNullEmail => Error.Validation(
            code: "Login.EmptyOrNullEmail",
            description: "Email cannot be empty or null");

        public static Error NotValidEmail => Error.Validation(
            code: "Login.NotValidEmail",
            description: "Email must be a valid email");

        public static Error EmptyOrNullPassword => Error.Validation(
            code: "Login.EmptyOrNullPassword",
            description: "Password cannot be empty or null");
    }
}