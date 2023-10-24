using ListToDo.Application.Common.Interfaces.Authentication;
using ListToDo.Application.Common.Interfaces.Services;
using ListToDo.Infrastructure.Authentication;
using ListToDo.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ListToDo.Application.Persistence;
using ListToDo.Infrastructure.Persistence;

namespace ListToDo.Infrastructure;
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