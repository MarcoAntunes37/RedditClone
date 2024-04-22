namespace RedditClone.Application.ReplyVotes.Commands.CreateReplyVote;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.ReplyVotes.Results.CreateReplyVoteResult;

public record CreateReplyVoteCommand(
    CommentId CommentId,
    ReplyId ReplyId,
    UserId UserId,
    bool IsVoted)
: IRequest<ErrorOr<CreateReplyVoteResult>>;