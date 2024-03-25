namespace RedditClone.Application.Comment.Commands.VoteOnReply;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Comment.Results.VoteOnReplyResult;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using Serilog;

public class VoteOnReplyCommandHandler
    : IRequestHandler<VoteOnReplyCommand, VoteOnReplyResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<VoteOnReplyCommand> _validator;
    private readonly IConfiguration _configuration;

    public VoteOnReplyCommandHandler(
        ICommentRepository commentRepository,
        IValidator<VoteOnReplyCommand> validator,
        IConfiguration configuration)
    {
        _commentRepository = commentRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<VoteOnReplyResult> Handle(VoteOnReplyCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message},{@VoteOnReplyCommand}",
            "Trying to vote on Reply: {@ReplyId}",
            command,
            command.ReplyId);

        _validator.ValidateAndThrow(command);

        _commentRepository.AddReplyVote(command.CommentId, command.ReplyId, command.UserId, command.IsVoted);

        VoteOnReplyResult result = new("Comment reply voted successfully");

        Log.Information(
            "{@VoteOnReplyResult}",
            result);

        return result;
    }
}