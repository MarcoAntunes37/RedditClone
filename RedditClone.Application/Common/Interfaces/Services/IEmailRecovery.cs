namespace RedditClone.Application.Common.Interfaces.Services;

public interface IEmailRecovery
{
    void SendEmail(string to, string subject, string body);
}