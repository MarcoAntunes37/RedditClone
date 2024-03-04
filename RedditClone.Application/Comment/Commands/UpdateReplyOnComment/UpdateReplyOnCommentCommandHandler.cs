namespace RedditClone.Application.Comment.Commands.UpdateReplyOnComment;

using FluentValidation;
using MediatR;
using RedditClone.Application.Comment.Results.UpdateReplyOnCommentResult;
using RedditClone.Application.Persistence;

public class UpdateReplyOnCommentCommandHandler :
    IRequestHandler<UpdateReplyOnCommentCommand, UpdateReplyOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<UpdateReplyOnCommentCommand> _validator;

    public UpdateReplyOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<UpdateReplyOnCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<UpdateReplyOnCommentResult> Handle(UpdateReplyOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _commentRepository.UpdateCommentReplyById(command.CommentId, command.ReplyId, command.UserId, command.Content);

        return new UpdateReplyOnCommentResult(
            "Comment reply updated successfully"
        );
    }
}