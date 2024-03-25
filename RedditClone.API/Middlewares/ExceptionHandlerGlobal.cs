namespace RedditClone.API.Middlewares;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Common.Errors;
using Serilog;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly IDictionary<HttpStatusCode, string> _statusCodeLinkMap = new Dictionary<HttpStatusCode, string>()
    {
        { HttpStatusCode.BadRequest, "https://www.rfc-editor.org/rfc/rfc2616#section-10.4.1" },
        { HttpStatusCode.Unauthorized, "https://www.rfc-editor.org/rfc/rfc2616#section-10.4.2" },
        { HttpStatusCode.NotFound, "https://www.rfc-editor.org/rfc/rfc2616#section-10.4.5" },
        { HttpStatusCode.Conflict, "https://www.rfc-editor.org/rfc/rfc2616#section-10.4.10" },
        { HttpStatusCode.UnsupportedMediaType, "https://www.rfc-editor.org/rfc/rfc2616#section-10.4.16" },
        { HttpStatusCode.InternalServerError, "https://www.rfc-editor.org/rfc/rfc2616#section-10.5.1" }
    };

    public ExceptionHandlerMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpCustomException exception)
        {
            var traceId = Guid.NewGuid();

            Log.Error(
                "Exception: {@Exception}, {@Source}, {@Message}, {@DateTimeUtc}",
                exception,
                exception.Message,
                exception.Source,
                DateTime.Now);

            var problemDetails = new ProblemDetails
            {
                Status = (int)exception.HttpStatusCode,
                Detail = exception.Message,
                Instance = GetMapLinkValue(exception.HttpStatusCode),
                Extensions = {
                    ["traceId"] = traceId}
            };

            context.Response.StatusCode = (int)exception.HttpStatusCode;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    public string GetMapLinkValue(HttpStatusCode code)
    {
        return _statusCodeLinkMap.FirstOrDefault(c => c.Key == code).Value;
    }
}