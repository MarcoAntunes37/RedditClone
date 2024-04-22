namespace RedditClone.API.Extension;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RedditClone.API.Endpoints;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] endpointServiceDescriptors = assembly
             .DefinedTypes
             .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                 type.IsAssignableTo(typeof(IEndpoint)))
             .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
             .ToArray();

        services.TryAddEnumerable(endpointServiceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> requiredServices = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder endpointRouteBuilder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in requiredServices)
        {
            endpoint.MapEndpoint(endpointRouteBuilder);
        }

        return app;
    }
}