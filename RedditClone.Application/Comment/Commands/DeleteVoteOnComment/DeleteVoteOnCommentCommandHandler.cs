namespace RedditClone.Application.Community.Commands.DeleteVoteOnComment;

using FluentValidation;
using MediatR;
using RedditClone.Application.Community.Results.DeleteVoteOnCommentResult;
using RedditClone.Application.Persistence;

public class DeleteVoteOnCommentCommandHandler :
    IRequestHandler<DeleteVoteOnCommentCommand, DeleteVoteOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<DeleteVoteOnCommentCommand> _validator;

    public DeleteVoteOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<DeleteVoteOnCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<DeleteVoteOnCommentResult> Handle(DeleteVoteOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _commentRepository.DeleteCommentVoteById(command.CommentId, command.VoteId, command.UserId);

        return new DeleteVoteOnCommentResult(
            "Comment successfully Deleted."
        );
    }
}