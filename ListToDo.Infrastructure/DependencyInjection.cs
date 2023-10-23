using ListToDo.Application.Common.Interfaces.Authentication;
using ListToDo.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace ListToDo.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}