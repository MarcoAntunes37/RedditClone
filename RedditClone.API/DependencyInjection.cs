using FluentValidation;
using RedditClone.Application.User.Commands.Register;
using RedditClone.Contracts.Register;

namespace RedditClone.API;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddControllerValidations();

        return services;
    }

    public static IServiceCollection AddControllerValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();

        return services;
    }
}