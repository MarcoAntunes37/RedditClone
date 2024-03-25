namespace RedditClone.Application.Post.Commands.DeletePost;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.DeletePostResult;
using Serilog;

public class DeletePostCommandHandler
    : IRequestHandler<DeletePostCommand, DeletePostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<DeletePostCommand> _validator;
    private readonly IConfiguration _configuration;

    public DeletePostCommandHandler(
        IPostRepository postRepository,
        IValidator<DeletePostCommand> validator,
        IConfiguration configuration)
    {
        _postRepository = postRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<DeletePostResult> Handle(DeletePostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@CreatePostCommand}",
            "Trying to delete post: {@PostId}",
            command.PostId);

        _validator.ValidateAndThrow(command);

        _postRepository.DeletePostById(command.PostId, command.UserId);

        DeletePostResult result = new("Post deleted successfully");

        Log.Information(
            "{@DeletePostResult}",
            result,
            command.PostId);

        return result;
    }
}