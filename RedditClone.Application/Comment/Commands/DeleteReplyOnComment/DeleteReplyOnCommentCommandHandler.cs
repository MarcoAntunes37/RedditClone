namespace RedditClone.Application.Community.Commands.DeleteReplyOnComment;

using FluentValidation;
using MediatR;
using RedditClone.Application.Community.Results.DeleteReplyOnCommentResult;
using RedditClone.Application.Persistence;

public class DeleteReplyOnCommentCommandHandler :
    IRequestHandler<DeleteReplyOnCommentCommand, DeleteReplyOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<DeleteReplyOnCommentCommand> _validator;

    public DeleteReplyOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<DeleteReplyOnCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<DeleteReplyOnCommentResult> Handle(DeleteReplyOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _commentRepository.DeleteCommentReplyById(command.CommentId, command.ReplyId, command.UserId);

        return new DeleteReplyOnCommentResult(
            "Comment reply successfully Deleted."
        );
    }
}