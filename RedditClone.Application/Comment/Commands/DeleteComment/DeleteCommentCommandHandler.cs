namespace RedditClone.Application.Community.Commands.DeleteComment;

using FluentValidation;
using MediatR;
using RedditClone.Application.Community.Results.DeleteCommentResult;
using RedditClone.Application.Persistence;

public class DeleteCommentCommandHandler :
    IRequestHandler<DeleteCommentCommand, DeleteCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<DeleteCommentCommand> _validator;

    public DeleteCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<DeleteCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<DeleteCommentResult> Handle(DeleteCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _commentRepository.DeleteCommentById(command.CommentId, command.UserId);

        return new DeleteCommentResult(
            "Comment successfully Deleted."
        );
    }
}