namespace RedditClone.API.Middlewares;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Errors;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
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
        ILogger<ExceptionHandlerMiddleware> logger,
        RequestDelegate next)
    {
        _logger = logger;
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
            _logger.LogError(
                $"Http status code: {exception.HttpStatusCode}",
                $"Exception occurred: {exception.Message}");

            var problemDetails = new ProblemDetails
            {
                Status = (int)exception.HttpStatusCode,
                Detail = exception.Message,
                Instance = GetMapLinkValue(exception.HttpStatusCode),
                Extensions = {
                    ["traceId"] = Guid.NewGuid()
                }
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