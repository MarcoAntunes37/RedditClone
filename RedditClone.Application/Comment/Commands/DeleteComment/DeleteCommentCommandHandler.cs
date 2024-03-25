namespace RedditClone.Application.Community.Commands.DeleteComment;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Community.Results.DeleteCommentResult;
using RedditClone.Application.Persistence;
using Serilog;

public class DeleteCommentCommandHandler :
    IRequestHandler<DeleteCommentCommand, DeleteCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<DeleteCommentCommand> _validator;
    private readonly IConfiguration _configuration;

    public DeleteCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<DeleteCommentCommand> validator,
        IConfiguration configuration)
    {
        _commentRepository = commentRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<DeleteCommentResult> Handle(DeleteCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@DeleteCommentCommand}",
            "Trying to delete comment {CommentId}",
            command,
            command.CommentId);

        _validator.ValidateAndThrow(command);

        _commentRepository.DeleteCommentById(command.CommentId, command.UserId);

        DeleteCommentResult result = new("Comment successfully deleted.");

        Log.Information(
           "{@DeleteCommentResult}, {@CommentId}",
           result,
           command.CommentId);

        return result;
    }
}