using RedditClone.Application.Common.Interfaces.Authentication;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Infrastructure.Authentication;
using RedditClone.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Persistence;
using RedditClone.Infrastructure.Persistence;

namespace RedditClone.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
    ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}