namespace RedditClone.Application.Post.Commands.VoteOnPost;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Persistence;
using RedditClone.Application.Post.Results.VoteOnPostResult;
using Serilog;

public class VoteOnPostCommandHandler
    : IRequestHandler<VoteOnPostCommand, VoteOnPostResult>
{
    private readonly IPostRepository _postRepository;
    private readonly IValidator<VoteOnPostCommand> _validator;
    private readonly IConfiguration _configuration;

    public VoteOnPostCommandHandler(
        IPostRepository postRepository,
        IValidator<VoteOnPostCommand> validator,
        IConfiguration configuration)
    {
        _postRepository = postRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<VoteOnPostResult> Handle(VoteOnPostCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@VoteOnPostCommand}",
            "Trying to vote on Post: {@PostId}",
            command,
            command.PostId);

        _validator.ValidateAndThrow(command);

        _postRepository.AddPostVote(command.PostId, command.UserId, command.IsVoted);

        VoteOnPostResult result = new("Vote on post successfully");

        Log.Information(
            "{@VoteOnPostResult}, {@PostId}",
            command,
            command.PostId);

        return result;
    }
}