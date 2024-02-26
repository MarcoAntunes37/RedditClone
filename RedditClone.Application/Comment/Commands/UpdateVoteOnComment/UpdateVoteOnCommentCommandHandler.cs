namespace RedditClone.Application.Community.Commands.UpdateVoteOnCommentCommand;

using FluentValidation;
using MediatR;
using RedditClone.Application.Comment.Commands.UpdateVoteOnComment;
using RedditClone.Application.Community.Results.UpdateVoteOnCommentResult;
using RedditClone.Application.Persistence;

public class UpdateVoteOnCommentCommandHandler :
    IRequestHandler<UpdateVoteOnCommentCommand, UpdateVoteOnCommentResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<UpdateVoteOnCommentCommand> _validator;

    public UpdateVoteOnCommentCommandHandler(
        ICommentRepository commentRepository,
        IValidator<UpdateVoteOnCommentCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<UpdateVoteOnCommentResult> Handle(UpdateVoteOnCommentCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _commentRepository.UpdateCommentVoteById(command.CommentId, command.VoteId, command.UserId, command.IsVoted);

        return new UpdateVoteOnCommentResult(
            "Comment successfully updated."
        );
    }
}