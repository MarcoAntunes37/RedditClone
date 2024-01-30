using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace RedditClone.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));
        return services;
    }
}