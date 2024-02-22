using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RedditClone.Application.Comment.Commands.CreateComment;
using RedditClone.Application.Comment.Commands.UpdateComment;
using RedditClone.Application.Community.Commands.CreateCommunity;
using RedditClone.Application.Community.Commands.DeleteComment;
using RedditClone.Application.Community.Commands.DeleteCommunity;
using RedditClone.Application.Community.Commands.TransferCommunity;
using RedditClone.Application.Community.Commands.UpdateCommunity;
using RedditClone.Application.Post.Commands.CreatePost;
using RedditClone.Application.Post.Commands.DeletePost;
using RedditClone.Application.Post.Commands.UpdatePost;
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
        services.AddCommunityValidations();
        services.AddPostValidations();
        services.AddCommentValidations();

        return services;
    }

    public static IServiceCollection AddCommentValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateCommentCommand>, CreateCommentCommandValidator>();
        services.AddScoped<IValidator<UpdateCommentCommand>, UpdateCommentCommandValidator>();
        services.AddScoped<IValidator<DeleteCommentCommand>, DeleteCommentCommandValidator>();

        return services;
    }

    public static IServiceCollection AddPostValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreatePostCommand>, CreatePostCommandValidator>();
        services.AddScoped<IValidator<UpdatePostCommand>, UpdatePostCommandValidator>();
        services.AddScoped<IValidator<DeletePostCommand>, DeletePostCommandValidator>();

        return services;
    }

    public static IServiceCollection AddCommunityValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateCommunityCommand>, CreateCommunityCommandValidator>();
        services.AddScoped<IValidator<UpdateCommunityCommand>, UpdateCommunityCommandValidator>();
        services.AddScoped<IValidator<TransferCommunityCommand>, TransferCommunityCommandValidator>();
        services.AddScoped<IValidator<DeleteCommunityCommand>, DeleteCommunityCommandValidator>();

        return services;
    }

    public static IServiceCollection AddUserValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
        services.AddScoped<IValidator<LoginQuery>, LoginQueryValidator>();
        services.AddScoped<IValidator<DeleteUserCommand>, DeleteUserCommandValidator>();
        services.AddScoped<IValidator<UpdateProfileCommand>, UpdateProfileCommandValidator>();
        services.AddScoped<IValidator<UpdatePasswordCommand>, UpdatePasswordCommandValidator>();

        return services;
    }
}