namespace RedditClone.Infrastructure.Service;

using RedditClone.Application.Common.Interfaces.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}