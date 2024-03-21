namespace RedditClone.Application.Helpers;

using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Logging;

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