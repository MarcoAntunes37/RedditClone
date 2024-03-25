namespace RedditClone.Application.Community.Commands.DeleteReplyOnComment;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Community.Results.DeleteReplyOnCommentResult;
using RedditClone.Application.Persistence;
using Serilog;

public class DeleteReplyOnCommentCommandHandler :
    IRequestHandler<DeleteReplyOnCommentCommand, DeleteReplyOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<DeleteReplyOnCommentCommand> _validator;
    private readonly IConfiguration _configuration;

    public DeleteReplyOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<DeleteReplyOnCommentCommand> validator,
        IConfiguration configuration)
    {
        _commentRepository = commentRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<DeleteReplyOnCommentResult> Handle(DeleteReplyOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@DeleteReplyOnCommentCommand}",
            "Trying delete Reply: {@ReplyId} on Comment: {@CommentId}",
            command,
            command.ReplyId,
            command.CommentId);

        _validator.ValidateAndThrow(command);

        _commentRepository.DeleteCommentReplyById(command.CommentId, command.ReplyId, command.UserId);

        DeleteReplyOnCommentResult result = new("Reply successfully delete.");

        Log.Information(
            "{@DeleteReplyOnCommentResult}",
            result);

        return result;
    }
}