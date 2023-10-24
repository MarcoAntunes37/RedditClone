using ListToDo.Application.Common.Interfaces.Services;

namespace ListToDo.Infrastructure.Service;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}