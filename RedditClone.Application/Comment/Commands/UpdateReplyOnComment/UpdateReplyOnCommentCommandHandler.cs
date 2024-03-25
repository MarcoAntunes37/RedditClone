namespace RedditClone.Application.Comment.Commands.UpdateReplyOnComment;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Comment.Results.UpdateReplyOnCommentResult;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Persistence;
using Serilog;

public class UpdateReplyOnCommentCommandHandler :
    IRequestHandler<UpdateReplyOnCommentCommand, UpdateReplyOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<UpdateReplyOnCommentCommand> _validator;
    private readonly IConfiguration _configuration;

    public UpdateReplyOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<UpdateReplyOnCommentCommand> validator,
        IConfiguration configuration)
    {
        _commentRepository = commentRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<UpdateReplyOnCommentResult> Handle(UpdateReplyOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@UpdateReplyOnCommentCommand}",
            "Trying to update Reply: {@ReplyId} on Comment: {@CommentId}",
            command,
            command.ReplyId,
            command.CommentId);

        _validator.ValidateAndThrow(command);

        _commentRepository.UpdateCommentReplyById(command.CommentId, command.ReplyId, command.UserId, command.Content);

        UpdateReplyOnCommentResult result = new("Comment reply updated successfully");

        Log.Information(
            "{@UpdateReplyOnCommentResult}",
            result);

        return result;
    }
}