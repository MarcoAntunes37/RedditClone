namespace RedditClone.Application.Comment.Commands.UpdateVoteOnReply;

using FluentValidation;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.Comment.Results.UpdateVoteOnReplyResult;

public class UpdateVoteOnReplyCommandHandler
    : IRequestHandler<UpdateVoteOnReplyCommand, UpdateVoteOnReplyResult>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<UpdateVoteOnReplyCommand> _validator;

    public UpdateVoteOnReplyCommandHandler(
        ICommentRepository commentRepository,
        IValidator<UpdateVoteOnReplyCommand> validator)
    {
        _commentRepository = commentRepository;
        _validator = validator;
    }

    public async Task<UpdateVoteOnReplyResult> Handle(UpdateVoteOnReplyCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _commentRepository.UpdateReplyVoteById(command.CommentId, command.ReplyId, command.VoteId, command.UserId, command.IsVoted);

        return new UpdateVoteOnReplyResult(
            "Comment reply vote updated successfully"
        );
    }
}