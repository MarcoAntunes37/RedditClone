namespace RedditClone.Application.Community.Commands.DeleteVoteOnComment;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Community.Results.DeleteVoteOnCommentResult;
using RedditClone.Application.Persistence;
using Serilog;

public class DeleteVoteOnCommentCommandHandler :
    IRequestHandler<DeleteVoteOnCommentCommand, DeleteVoteOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<DeleteVoteOnCommentCommand> _validator;
    private readonly IConfiguration _configuration;

    public DeleteVoteOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<DeleteVoteOnCommentCommand> validator,
        IConfiguration configuration)
    {
        _commentRepository = commentRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<DeleteVoteOnCommentResult> Handle(DeleteVoteOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@DeleteVoteOnCommentCommand}",
            "Trying to delete Vote: {@VoteId} on Comment: {@CommentId}",
            command,
            command.VoteId,
            command.CommentId);

        _validator.ValidateAndThrow(command);

        _commentRepository.DeleteCommentVoteById(command.CommentId, command.VoteId, command.UserId);

        DeleteVoteOnCommentResult result = new("Comment successfully deleted.");

        Log.Information(
            "{@DeleteVoteOnCommentResult}",
            result);

        return result;
    }
}