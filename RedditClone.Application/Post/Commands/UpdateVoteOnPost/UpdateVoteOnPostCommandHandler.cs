namespace RedditClone.Application.Post.Commands.UpdateVoteOnPost;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.UpdateVoteOnPostResult;
using Serilog;

public class UpdateVoteOnPostCommandHandler
    : IRequestHandler<UpdateVoteOnPostCommand, UpdateVoteOnPostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<UpdateVoteOnPostCommand> _validator;
    private readonly IConfiguration _configuration;

    public UpdateVoteOnPostCommandHandler(
        IPostRepository postRepository,
        IValidator<UpdateVoteOnPostCommand> validator,
        IConfiguration configuration)
    {
        _postRepository = postRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<UpdateVoteOnPostResult> Handle(UpdateVoteOnPostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@UpdateVoteOnPostCommand}",
            "Trying to update Vote: {@VoteId} on Post: {@PostId}",
            command.VoteId,
            command.PostId);

        _validator.ValidateAndThrow(command);

        _postRepository.UpdatePostVoteById(command.PostId, command.VoteId, command.UserId, command.IsVoted);

        UpdateVoteOnPostResult result = new ("Vote on post updated successfully");

        Log.Information(
            "{@UpdateVoteOnPostResult}, {@VoteId}",
            result,
            command.VoteId);

        return result;
    }
}