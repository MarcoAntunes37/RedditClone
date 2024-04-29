namespace RedditClone.Application;

using FluentValidation;
using RedditClone.Application.Behaviors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedditClone.Application.User.Queries.Login;
using RedditClone.Application.User.Commands.Delete;
using RedditClone.Application.User.Commands.Update;
using RedditClone.Application.User.Commands.Register;
using RedditClone.Application.Post.Commands.CreatePost;
using RedditClone.Application.Post.Commands.DeletePost;
using RedditClone.Application.Post.Commands.UpdatePost;
using RedditClone.Application.Post.Queries.GetPostById;
using RedditClone.Application.User.Commands.UpdateProfile;
using RedditClone.Application.User.Commands.UpdatePassword;
using RedditClone.Application.PostVotes.Commands.CreateVote;
using RedditClone.Application.PostVotes.Commands.DeleteVote;
using RedditClone.Application.PostVotes.Commands.UpdateVote;
using RedditClone.Application.Comment.Commands.CreateComment;
using RedditClone.Application.Comment.Queries.GetCommentById;
using RedditClone.Application.Comment.Commands.DeleteComment;
using RedditClone.Application.Comment.Commands.UpdateComment;
using RedditClone.Application.Community.Commands.CreateCommunity;
using RedditClone.Application.Community.Commands.UpdateCommunity;
using RedditClone.Application.Community.Commands.DeleteCommunity;
using RedditClone.Application.ReplyVotes.Commands.CreateReplyVote;
using RedditClone.Application.ReplyVotes.Commands.UpdateReplyVote;
using RedditClone.Application.ReplyVotes.Commands.DeleteReplyVote;
using RedditClone.Application.Community.Commands.GetCommunityById;
using RedditClone.Application.Comment.Queries.GetCommentsByPostId;
using RedditClone.Application.Community.Queries.GetCommunitiesById;
using RedditClone.Application.Post.Queries.GetPostListByCommunityId;
using RedditClone.Application.User.Commands.SendPasswordRecoveryEmail;
using RedditClone.Application.CommentVotes.Commands.CreateCommentVote;
using RedditClone.Application.CommentVotes.Commands.DeleteCommentVote;
using RedditClone.Application.CommentVotes.Commands.UpdateCommentVote;
using RedditClone.Application.User.Commands.PasswordRecoveryNewPassword;
using RedditClone.Application.Community.Queries.GetPostListByCommunityId;
using RedditClone.Application.User.Commands.PasswordRecoveryCodeValidate;
using RedditClone.Application.UserCommunities.Commands.UserJoinACommunity;
using RedditClone.Application.UserCommunities.Commands.UserLeftACommunity;
using RedditClone.Application.CommentReplies.Commands.CreateCommentReply;
using RedditClone.Application.CommentReplies.Commands.UpdateCommentReply;
using RedditClone.Application.CommentReplies.Commands.DeleteCommentReply;
using RedditClone.Application.UserCommunities.Queries.GetUserListByCommunityId;
using RedditClone.Application.User.Commands.PasswordRecoveryNewPasswordCommandValidator;
using RedditClone.Application.UserCommunities.Queries.GetCommunitiesListByUserId;
using RedditClone.Application.Comment.Queries.GetCommentsListByPostId;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
    ConfigurationManager configuration)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            x.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            x.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidation();

        return services;
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddUserValidations();
        services.AddCommunityValidations();
        services.UserCommunityValidations();
        services.AddPostValidations();
        services.AddPostVotesValidations();
        services.AddCommentValidations();

        return services;
    }

    public static IServiceCollection AddCommentValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<GetCommentByIdQuery>, GetCommentByIdQueryValidator>();
        services.AddScoped<IValidator<GetCommentsListByPostIdQuery>, GetCommentsListByPostIdQueryValidator>();

        services.AddScoped<IValidator<CreateCommentCommand>, CreateCommentCommandValidator>();
        services.AddScoped<IValidator<UpdateCommentCommand>, UpdateCommentCommandValidator>();
        services.AddScoped<IValidator<DeleteCommentCommand>, DeleteCommentCommandValidator>();

        return services;
    }


    public static IServiceCollection AddCommentVotesValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateCommentVoteCommand>, CreateCommentVoteCommandValidator>();
        services.AddScoped<IValidator<UpdateCommentVoteCommand>, UpdateCommentVoteCommandValidator>();
        services.AddScoped<IValidator<DeleteCommentVoteCommand>, DeleteCommentVoteCommandValidator>();

        return services;
    }

    public static IServiceCollection AddCommentRepliesValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateCommentReplyCommand>, CreateCommentReplyCommandValidator>();
        services.AddScoped<IValidator<UpdateCommentReplyCommand>, UpdateCommentReplyCommandValidator>();
        services.AddScoped<IValidator<DeleteCommentReplyCommand>, DeleteCommentReplyCommandValidator>();

        return services;
    }

    public static IServiceCollection AddRepliesVotesValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateReplyVoteCommand>, CreateReplyVoteCommandValidator>();
        services.AddScoped<IValidator<UpdateReplyVoteCommand>, UpdateReplyVoteCommandValidator>();
        services.AddScoped<IValidator<DeleteReplyVoteCommand>, DeleteReplyVoteCommandValidator>();

        return services;
    }

    public static IServiceCollection AddPostValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<GetPostByIdQuery>, GetPostByIdQueryValidator>();
        services.AddScoped<IValidator<GetPostListByCommunityIdQuery>, GetPostListByCommunityIdQueryValidator>();
        services.AddScoped<IValidator<CreatePostCommand>, CreatePostCommandValidator>();
        services.AddScoped<IValidator<UpdatePostCommand>, UpdatePostCommandValidator>();
        services.AddScoped<IValidator<DeletePostCommand>, DeletePostCommandValidator>();

        return services;
    }

    public static IServiceCollection AddPostVotesValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreatePostVoteCommand>, CreatePostVoteCommandValidator>();
        services.AddScoped<IValidator<UpdatePostVoteCommand>, UpdatePostVoteCommandValidator>();
        services.AddScoped<IValidator<DeletePostVoteCommand>, DeletePostVoteCommandValidator>();

        return services;
    }

    public static IServiceCollection UserCommunityValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<UserJoinACommunityCommand>, UserJoinACommunityCommandValidator>();
        services.AddScoped<IValidator<UserLeftACommunityCommand>, UserLeftACommunityCommandValidator>();
        services.AddScoped<IValidator<GetUserListByCommunityIdQuery>, GetUserListByCommunityIdQueryValidator>();
        services.AddScoped<IValidator<GetCommunitiesListByUserIdQuery>, GetCommunitiesListByUserIdQueryValidator>();

        return services;
    }
    public static IServiceCollection AddCommunityValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<GetCommunityByIdQuery>, GetCommunityByIdQueryValidator>();
        services.AddScoped<IValidator<CreateCommunityCommand>, CreateCommunityCommandValidator>();
        services.AddScoped<IValidator<UpdateCommunityCommand>, UpdateCommunityCommandValidator>();
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
        services.AddScoped<IValidator<SendPasswordRecoveryEmailCommand>, SendPasswordRecoveryEmailCommandValidator>();
        services.AddScoped<IValidator<PasswordRecoveryNewPasswordCommand>, PasswordRecoveryNewPasswordCommandValidator>();
        services.AddScoped<IValidator<PasswordRecoveryCodeValidateCommand>, PasswordRecoveryCodeValidateCommandValidator>();

        return services;
    }
}