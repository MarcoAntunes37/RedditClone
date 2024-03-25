namespace RedditClone.Application.Common.Helpers;

using Microsoft.Extensions.Configuration;
using Serilog;

public class SerilogLoggerConfiguration
{
    public readonly IConfiguration _configuration;
    public SerilogLoggerConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void CreateLogger()
    {
        new LoggerConfiguration()
            .ReadFrom.Configuration(_configuration)
            .CreateLogger();
    }
}