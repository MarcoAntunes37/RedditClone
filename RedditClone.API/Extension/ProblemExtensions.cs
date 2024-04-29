namespace RedditClone.API.Extension;

using ErrorOr;

public static class ProblemExtensions
{
    public static IResult CreateProblemDetails(List<Error> errors)
    {
        return Results.Problem(
            detail: errors.First().Description,
            title: errors.First().Code,
            instance: Environment.CurrentDirectory,
            statusCode: StatusCodeExtensions.GetStatusCode(errors.First().Type.ToString()),
            extensions: new Dictionary<string, object>()
            {
                ["stackTrace"] = Environment.StackTrace,
                ["traceId"] = Guid.NewGuid()
            }!
        );
    }
}