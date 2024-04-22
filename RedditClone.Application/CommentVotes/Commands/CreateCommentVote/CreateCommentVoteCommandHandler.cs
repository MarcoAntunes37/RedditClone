namespace RedditClone.Application.CommentVotes.Commands.CreateCommentVote;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.CommentVotes.Results.CreateCommentVoteResult;

public class CreateCommentVoteCommandHandler(
    ICommentRepository commentRepository)
        : IRequestHandler<CreateCommentVoteCommand, ErrorOr<CreateCommentVoteResult>>
{
    private readonly ICommentRepository _commentRepository = commentRepository;

    public async Task<ErrorOr<CreateCommentVoteResult>> Handle(
        CreateCommentVoteCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message},{@VoteOnCommentCommand}",
            "Trying to vote on Comment: {@CommentId}",
            command,
            command.CommentId);

        if (_commentRepository.GetCommentById(command.CommentId).Value is null)
        {
            Error error = Errors.Comments.CommentNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        _commentRepository.AddCommentVote(command.CommentId, command.UserId, command.IsVoted);

        CreateCommentVoteResult result = new("Vote successfully on comment");

        Log.Information(
            "{@VoteOnCommentResult}",
            result);

        return result;
    }
}