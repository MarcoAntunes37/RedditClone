namespace RedditClone.API.Extension;

public static class StatusCodeExtensions
{
    private static readonly Dictionary<string, int> ErrorCodes = new()
    {
        { "Validation", 400 },
        { "Conflict", 409 },
        { "NotFound", 404 },
        { "Unauthorized", 401 },
        { "Forbidden", 403 }
    };
    public static int GetStatusCode(string errorType)
    {
        return ErrorCodes[errorType];
    }
}