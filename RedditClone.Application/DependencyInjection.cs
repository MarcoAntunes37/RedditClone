using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RedditClone.Application.Comment.Commands.CreateCommentCommand;
using RedditClone.Application.Community.Commands.CreateCommunity;
using RedditClone.Application.Post.Commands.CreatePost;
using RedditClone.Application.User.Commands.Delete;
using RedditClone.Application.User.Commands.Register;
using RedditClone.Application.User.Commands.Update;
using RedditClone.Application.User.Commands.UpdatePassword;
using RedditClone.Application.User.Commands.UpdateProfile;
using RedditClone.Application.User.Queries.Login;

namespace RedditClone.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(x =>
            x.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));

        services.AddValidation();

        return services;
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddUserValidations();
        services.AddScoped<IValidator<CreateCommunityCommand>, CreateCommunityCommandValidator>();
        services.AddScoped<IValidator<CreatePostCommand>, CreatePostCommandValidator>();
        services.AddScoped<IValidator<CreateCommentCommand>, CreateCommentCommandValidator>();

        return services;
    }

    public static IServiceCollection AddUserValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
        services.AddScoped<IValidator<LoginQuery>, LoginQueryValidator>();
        services.AddScoped<IValidator<DeleteCommand>, DeleteCommandValidator>();
        services.AddScoped<IValidator<UpdateProfileCommand>, UpdateProfileCommandValidator>();
        services.AddScoped<IValidator<UpdatePasswordCommand>, UpdatePasswordCommandValidator>();

        return services;
    }
}