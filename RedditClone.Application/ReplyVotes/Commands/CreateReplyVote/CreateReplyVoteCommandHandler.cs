namespace RedditClone.Application.ReplyVotes.Commands.CreateReplyVote;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.ReplyVotes.Results.CreateReplyVoteResult;

public class CreateReplyVoteCommandHandler(
    ICommentRepository commentRepository)
        : IRequestHandler<CreateReplyVoteCommand, ErrorOr<CreateReplyVoteResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<ErrorOr<CreateReplyVoteResult>> Handle(
        CreateReplyVoteCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message},{@ReplyVoteCommand}",
            "Trying to vote on Reply: {@ReplyId}",
            command,
            command.ReplyId);

        if(_commentRepository.GetCommentById(command.CommentId).Value is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        if(!_commentRepository.UserExists(command.UserId))
        {
            Error error = Errors.User.UserNotFound;
            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        if(!_commentRepository.CommentReplyExists(command.CommentId, command.ReplyId))
        {
            Error error = Errors.ReplyVotes.VoteNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        _commentRepository.AddReplyVote(command.CommentId, command.ReplyId, command.UserId, command.IsVoted);

        CreateReplyVoteResult result = new("Comment reply voted successfully");

        Log.Information(
            "{@ReplyVoteResult}",
            result);

        return result;
    }
}