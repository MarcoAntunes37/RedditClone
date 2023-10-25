using ListToDo.Application.Services.Authentication;
using ListToDo.Application.Services.Authentication.Commands;
using ListToDo.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace ListToDo.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationCommandsService, AuthenticationCommandsService>();
        services.AddScoped<IAuthenticationQueriesService, AuthenticationQueriesService>();

        return services;
    }
}