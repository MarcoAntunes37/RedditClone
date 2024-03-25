namespace RedditClone.Application.Community.Commands.DeleteVoteOnReply;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Community.Results.DeleteVoteOnReplyResult;
using RedditClone.Application.Persistence;
using Serilog;

public class DeleteVoteOnReplyCommandHandler :
    IRequestHandler<DeleteVoteOnReplyCommand, DeleteVoteOnReplyResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<DeleteVoteOnReplyCommand> _validator;
    private readonly IConfiguration _configuration;

    public DeleteVoteOnReplyCommandHandler(
        ICommentRepository commentRepository,
        IValidator<DeleteVoteOnReplyCommand> validator,
        IConfiguration configuration)
    {
        _commentRepository = commentRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<DeleteVoteOnReplyResult> Handle(DeleteVoteOnReplyCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@DeleteVoteOnReplyCommand}",
            "Trying to delete Vote: {@VoteId} on Reply: {@ReplyId}",
            command,
            command.VoteId,
            command.ReplyId);

        _validator.ValidateAndThrow(command);

        _commentRepository.DeleteReplyVoteById(command.CommentId, command.ReplyId, command.VoteId, command.UserId);

        DeleteVoteOnReplyResult result = new("Vote deleted successfully.");

        Log.Information(
            "{@DeleteVoteOnReplyResult}",
            result);

        return result;
    }
}