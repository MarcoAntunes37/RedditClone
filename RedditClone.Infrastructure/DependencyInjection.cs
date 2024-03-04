using RedditClone.Application.Common.Interfaces.Authentication;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Infrastructure.Authentication;
using RedditClone.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Persistence;
using RedditClone.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using RedditClone.Infrastructure.Services;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Infrastructure.Persistence.Repositories;

namespace RedditClone.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddDatabase(configuration);
        services.AddPersistence();
        services.AddSingleton<IRecoveryCodeManager, RecoveryCodeManager>();
        services.AddSingleton<IEmailRecovery, EmailRecovery>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommunityRepository, CommunityRepository>();
        services.AddScoped<IUserCommunitiesRepository, UserCommunitiesRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services,
            ConfigurationManager configuration)
    {
        var dbSettings = new DbSettings();

        configuration.Bind(DbSettings.SectionName, dbSettings);

        services.AddSingleton(Options.Create(dbSettings));

        services.AddDbContext<RedditCloneDbContext>(options =>
            options.UseNpgsql(
                $"Host={dbSettings.Host};" +
                $"Port={dbSettings.Port};" +
                $"Database={dbSettings.DB};" +
                $"Username={dbSettings.Username};" +
                $"Password={dbSettings.Password};"
            ));

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
            };
        });

        return services;
    }
}