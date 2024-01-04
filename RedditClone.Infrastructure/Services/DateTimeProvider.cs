using RedditClone.Application.Common.Interfaces.Services;

namespace RedditClone.Infrastructure.Service;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}