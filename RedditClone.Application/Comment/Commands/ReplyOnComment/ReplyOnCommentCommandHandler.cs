namespace RedditClone.Application.Comment.Commands.ReplyOnComment;

using FluentValidation;
using MediatR;
using RedditClone.Application.Comment.Results.ReplyOnCommentResult;
using RedditClone.Application.Persistence;

public class ReplyOnCommentCommandHandler :
    IRequestHandler<ReplyOnCommentCommand, ReplyOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<ReplyOnCommentCommand> _validator;

    public ReplyOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<ReplyOnCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<ReplyOnCommentResult> Handle(ReplyOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _commentRepository.AddCommentReply(command.CommentId, command.UserId, command.Content);

        return new ReplyOnCommentResult(
            "Comment replied successfully"
        );
    }
}