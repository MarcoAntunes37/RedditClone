namespace RedditClone.Application.Community.Commands.UpdateVoteOnCommentCommand;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Comment.Commands.UpdateVoteOnComment;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Community.Results.UpdateVoteOnCommentResult;
using RedditClone.Application.Persistence;
using Serilog;

public class UpdateVoteOnCommentCommandHandler :
    IRequestHandler<UpdateVoteOnCommentCommand, UpdateVoteOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<UpdateVoteOnCommentCommand> _validator;
    private readonly IConfiguration _configuration;

    public UpdateVoteOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<UpdateVoteOnCommentCommand> validator,
        IConfiguration configuration)
    {
        _commentRepository = commentRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<UpdateVoteOnCommentResult> Handle(UpdateVoteOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@UpdateVoteOnCommentCommand}",
            "Trying to update Vote: {@VoteId} on Comment: {CommentId}",
            command,
            command.VoteId,
            command.CommentId);

        _validator.ValidateAndThrow(command);

        _commentRepository.UpdateCommentVoteById(command.CommentId, command.VoteId, command.UserId, command.IsVoted);

        UpdateVoteOnCommentResult result = new("Comment successfully updated.");

        Log.Information(
            "{@UpdateVoteOnCommentResult}",
            result);

        return result;
    }
}