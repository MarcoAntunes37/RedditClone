namespace RedditClone.Application.Comment.Commands.VoteOnComment;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Comment.Results.VoteOnCommentResult;

public class VoteOnCommentCommandHandler
    : IRequestHandler<VoteOnCommentCommand, VoteOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<VoteOnCommentCommand> _validator;

    public VoteOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<VoteOnCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<VoteOnCommentResult> Handle(VoteOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _commentRepository.AddCommentVote(command.CommentId, command.UserId, command.IsVoted);

        return new VoteOnCommentResult(
            "Comment updated successfully"
        );
    }
}