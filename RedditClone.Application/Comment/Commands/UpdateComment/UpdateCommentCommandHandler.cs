namespace RedditClone.Application.Community.Commands.UpdateComment;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Comment.Commands.UpdateComment;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Community.Results.UpdateCommentResult;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommentAggregate;
using Serilog;

public class UpdateCommentCommandHandler :
    IRequestHandler<UpdateCommentCommand, UpdateCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<UpdateCommentCommand> _validator;
    private readonly IConfiguration _configuration;

    public UpdateCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<UpdateCommentCommand> validator,
        IConfiguration configuration)
    {
        _commentRepository = commentRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<UpdateCommentResult> Handle(UpdateCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@UpdateCommentCommand}",
            "Trying to update Comment: {@CommentId}",
            command,
            command.CommentId);

        _validator.ValidateAndThrow(command);

        Comment comment = _commentRepository.UpdateCommentById(command.CommentId, command.UserId, command.Content);

        UpdateCommentResult result = new("Comment successfully updated.", comment);

        Log.Information(
            "{@UpdateCommentResult}",
            result);

        return result;
    }
}