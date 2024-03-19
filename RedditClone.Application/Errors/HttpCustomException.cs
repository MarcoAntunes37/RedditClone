namespace RedditClone.Application.Errors;

using System.Net;

public class HttpCustomException : Exception
{
    public HttpStatusCode HttpStatusCode { get; set; }

    public HttpCustomException(HttpStatusCode httpStatusCode, string message)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }
}