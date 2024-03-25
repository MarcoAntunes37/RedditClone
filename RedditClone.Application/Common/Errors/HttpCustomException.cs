namespace RedditClone.Application.Common.Errors;

using System.Net;

public class HttpCustomException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }

    public HttpCustomException(HttpStatusCode httpStatusCode, string? message)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }

    public HttpCustomException() : base()
    {
    }

    public HttpCustomException(string? message) : base(message)
    {
    }

    public HttpCustomException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}