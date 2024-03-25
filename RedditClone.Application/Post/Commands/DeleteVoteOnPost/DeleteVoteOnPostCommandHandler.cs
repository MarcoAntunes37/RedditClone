namespace RedditClone.Application.Post.Commands.DeleteVoteOnPost;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.DeleteVoteOnPostResult;
using Serilog;

public class DeleteVoteOnPostCommandHandler
    : IRequestHandler<DeleteVoteOnPostCommand, DeleteVoteOnPostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<DeleteVoteOnPostCommand> _validator;
    private readonly IConfiguration _configuration;

    public DeleteVoteOnPostCommandHandler(
        IPostRepository postRepository,
        IValidator<DeleteVoteOnPostCommand> validator, IConfiguration configuration)
    {
        _postRepository = postRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<DeleteVoteOnPostResult> Handle(DeleteVoteOnPostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@DeleteVoteOnPostCommand}",
            "Trying to delete Vote: {@VoteId} in Post: {@PostId}",
            command,
            command.VoteId,
            command.PostId);

        _validator.ValidateAndThrow(command);

        _postRepository.DeletePostVoteById(command.PostId, command.VoteId, command.UserId);

        DeleteVoteOnPostResult result = new("Vote on post deleted successfully");

        Log.Information(
            "{@DeleteVoteOnPostResult}, {@VoteId}",
            result,
            command.VoteId);

        return result;
    }
}