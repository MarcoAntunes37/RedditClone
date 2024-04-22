namespace RedditClone.Application.CommentVotes.Commands.CreateCommentVote;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentVotes.Results.CreateCommentVoteResult;

public record CreateCommentVoteCommand(
    CommentId CommentId,
    UserId UserId,
    bool IsVoted)
: IRequest<ErrorOr<CreateCommentVoteResult>>;