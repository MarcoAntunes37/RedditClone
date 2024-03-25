namespace RedditClone.Application.Comment.Commands.VoteOnComment;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Comment.Results.VoteOnCommentResult;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using Serilog;

public class VoteOnCommentCommandHandler
    : IRequestHandler<VoteOnCommentCommand, VoteOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<VoteOnCommentCommand> _validator;
    private readonly IConfiguration _configuration;

    public VoteOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<VoteOnCommentCommand> validator,
        IConfiguration configuration)
    {
        _commentRepository = commentRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<VoteOnCommentResult> Handle(VoteOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message},{@VoteOnCommentCommand}",
            "Trying to vote on Comment: {@CommentId}",
            command,
            command.CommentId);

        _validator.ValidateAndThrow(command);

        _commentRepository.AddCommentVote(command.CommentId, command.UserId, command.IsVoted);

        VoteOnCommentResult result = new("Vote successfully on comment");

        Log.Information(
            "{@VoteOnCommentResult}",
            result);

        return result;
    }
}