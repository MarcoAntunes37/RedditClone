namespace RedditClone.Application.ReplyVotes.Commands.UpdateReplyVote;

using MediatR;
using ErrorOr;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.ReplyVotes.Results.UpdateReplyVoteResult;

public class UpdateReplyVoteCommandHandler(
    ICommentRepository commentRepository)
        : IRequestHandler<UpdateReplyVoteCommand, ErrorOr<UpdateReplyVoteResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<ErrorOr<UpdateReplyVoteResult>> Handle(
        UpdateReplyVoteCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if(_commentRepository.GetCommentById(command.CommentId).Value is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        _commentRepository.UpdateReplyVoteById(command.CommentId, command.ReplyId, command.VoteId, command.UserId, command.IsVoted);

        UpdateReplyVoteResult result = new("Comment reply vote updated successfully");

        return result;
    }
}