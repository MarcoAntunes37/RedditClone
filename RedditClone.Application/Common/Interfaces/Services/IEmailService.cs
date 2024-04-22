namespace RedditClone.Application.Common.Interfaces.Services;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string to, string username, CancellationToken cancellationToken = default);
    Task SendRecoveryEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
}