namespace RedditClone.Application.Post.Commands.UpdatePost;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.UpdatePostResult;
using Serilog;
using RedditClone.Domain.PostAggregate;

public class UpdatePostCommandHandler
    : IRequestHandler<UpdatePostCommand, UpdatePostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<UpdatePostCommand> _validator;
    private readonly IConfiguration _configuration;

    public UpdatePostCommandHandler(
        IPostRepository postRepository,
        IValidator<UpdatePostCommand> validator, IConfiguration configuration)
    {
        _postRepository = postRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<UpdatePostResult> Handle(UpdatePostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@Command}",
            "Trying to update Post: {@PostId}",
            command,
            command.PostId);

        _validator.ValidateAndThrow(command);

        Post post = _postRepository.UpdatePostById(command.PostId, command.UserId, command.Title, command.Content);

        UpdatePostResult result = new("Post updated successfully",post);

        Log.Information(
            "{@UpdatePostResult}");

        return result;
    }
}