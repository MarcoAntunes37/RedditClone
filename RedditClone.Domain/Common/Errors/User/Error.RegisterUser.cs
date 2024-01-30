using ErrorOr;

namespace RedditClone.Domain.Common.Errors.User;

public static partial class Errors
{
    public static class RegisterUser
    {
        public static Error EmptyOrNullEmail => Error.Validation(
            code: "RegisterUser.EmptyOrNullEmail",
            description: "Email cannot be empty or null");

        public static Error NotValidEmail => Error.Validation(
            code: "Register.NotValidEmail",
            description: "Email must be a valid email");

        public static Error DuplicatedEmail => Error.Conflict(
            code: "RegisterUser.DuplicatedEmail",
            description: "Email is already in use");

        public static Error DuplicatedUsername => Error.Conflict(
            code: "RegisterUser.DuplicateUsername",
            description: "Username is already in use");

        public static Error EmptyOrNullFirstName => Error.Validation(
            code: "RegisterUser.EmptyOrNullFirstName",
            description: "Firstname cannot be empty or null");

        public static Error EmptyOrNullLastName => Error.Validation(
            code: "RegisterUser.EmptyOrNullLastName",
            description: "Lastname cannot be empty or null");

        public static Error EmptyOrNullUsername => Error.Validation(
            code: "RegisterUser.EmptyOrNullUsername",
            description: "Username cannot be empty or null");

        public static Error EmptyOrNullPassword => Error.Validation(
            code: "RegisterUser.EmptyOrNullPassword",
            description: "Password cannot be empty or null");
    }
}