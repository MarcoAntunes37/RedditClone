namespace RedditClone.Application.Post.Commands.CreatePost;

using System.Net;
using FluentValidation;
using MediatR;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.CreatePostResult;
using RedditClone.Domain.PostAggregate;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using Serilog;

public class CreatePostCommandHandler
    : IRequestHandler<CreatePostCommand, CreatePostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserCommunitiesRepository _userCommunitiesRepository;
    private readonly IValidator<CreatePostCommand> _validator;
    private readonly IConfiguration _configuration;

    public CreatePostCommandHandler(
        IPostRepository postRepository,
        IUserCommunitiesRepository userCommunitiesRepository,
        IValidator<CreatePostCommand> validator,
        IConfiguration configuration)
    {
        _postRepository = postRepository;
        _userCommunitiesRepository = userCommunitiesRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<CreatePostResult> Handle(CreatePostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@CreatePostCommand}",
            "Trying to create post in community {@CommunityId}",
            command,
            command.CommunityId);

        Log.Information(
            "{@Message}",
            "Checking if User: {@UserId} is part of Community: {@CommunityId}",
            command.UserId,
            command.CommunityId);

        bool isValid = _userCommunitiesRepository.ValidateRelationship(command.UserId, command.CommunityId);

        if(!isValid)
            throw new HttpCustomException(HttpStatusCode.NotFound, "User is not part of this community");

        Log.Information("User is part of community");

        _validator.ValidateAndThrow(command);

        var post = Post.Create(
            command.CommunityId,
            command.UserId,
            command.Title,
            command.Content,
            command.CreatedAt,
            command.UpdatedAt,
            command.Votes
        );

        _postRepository.Add(post);

        CreatePostResult result = new(
            post);

        Log.Information(
            "{@Message}, {@CreatePostResult}",
            "Post created successfully",
            result);

        return result;
    }
}