namespace RedditClone.Infrastructure.Common.Errors;

using ErrorOr;
public static class Errors
{
    public static class SmtpServer
    {
        public static Error EmailNotSent => Error.Failure(code: "SmtpServer.EmailNotSent", description: "Email not sent");
    }
}