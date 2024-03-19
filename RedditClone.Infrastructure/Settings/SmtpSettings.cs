namespace RedditClone.Infrastructure.Settings;

public class SmtpSettings
{
    public const string SectionName = "SmtpSettings";
    public string Host { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public int Port { get; init; } = 0;
}