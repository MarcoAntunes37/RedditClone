using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ListToDo.API.Filters;

public class ErrroHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        var problemDetails = new ProblemDetails{
            Title = "An error occurred while processing your request.",
            Status = (int)HttpStatusCode.InternalServerError
        };

        var errorResult = new { error = "An error occurred while processing your request." };

        context.Result = new ObjectResult(errorResult){
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}