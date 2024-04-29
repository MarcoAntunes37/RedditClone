namespace RedditClone.Infrastructure;

using Quartz;
using System.Text;
using Rebus.Config;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using RedditClone.Infrastructure.Jobs;
using RedditClone.Infrastructure.Service;
using Microsoft.Extensions.Configuration;
using RedditClone.Infrastructure.Services;
using RedditClone.Application.Persistence;
using RedditClone.Infrastructure.Settings;
using RedditClone.Infrastructure.Persistence;
using RedditClone.Infrastructure.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using RedditClone.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Infrastructure.Persistence.Repositories;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Common.Interfaces.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddAuthorization();
        services.AddDatabase(configuration);
        services.AddRebusConfig(configuration);
        services.AddPersistence();
        services.AddSingleton<IRecoveryCodeManager, RecoveryCodeManager>();
        services.AddSingleton<IEmailService, EmailService>();
        services.AddEmailRecovery(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddEmailRecovery(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var smtpSettings = new SmtpSettings();

        configuration.Bind(SmtpSettings.SectionName, smtpSettings);

        services.AddSingleton(Microsoft.Extensions.Options.Options.Create(smtpSettings));

        services.Configure<SmtpSettings>(
            configuration.GetSection(SmtpSettings.SectionName));

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
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var dbSettings = new DbSettings();

        configuration.Bind(DbSettings.SectionName, dbSettings);

        services.AddSingleton(Microsoft.Extensions.Options.Options.Create(dbSettings));

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddDbContext<RedditCloneDbContext>((sp, options) =>
        {
            options.UseNpgsql(
                            $"Host={dbSettings.Host};" +
                            $"Port={dbSettings.Port};" +
                            $"Database={dbSettings.DB};" +
                            $"Username={dbSettings.Username};" +
                            $"Password={dbSettings.Password};");

            var interceptors = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>()!;

            options.AddInterceptors(interceptors);
        });

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger =>
                        trigger
                            .ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule
                                    .WithIntervalInSeconds(10)
                                    .WithRepeatCount(10)));

        });

        services.AddQuartzHostedService();

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Microsoft.Extensions.Options.Options.Create(jwtSettings));
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

    public static IServiceCollection AddRebusConfig(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var rebusSettings = new RebusSettings();

        configuration.Bind(RebusSettings.SectionName, rebusSettings);

        services.AddSingleton(Microsoft.Extensions.Options.Options.Create(rebusSettings));

        services.Configure<RebusSettings>(
            configuration.GetSection(RebusSettings.SectionName));

        services.AddRebus(configure => configure
            .Logging(l => l.Serilog())
            .Transport(t =>
                t.UseRabbitMqAsOneWayClient(rebusSettings.ServerUrl)), false);

        return services;
    }
}