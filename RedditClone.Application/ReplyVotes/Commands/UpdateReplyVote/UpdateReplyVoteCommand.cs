namespace RedditClone.Application.ReplyVotes.Commands.UpdateReplyVote;

using MediatR;
using ErrorOr;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.ReplyVotes.Results.UpdateReplyVoteResult;

public record UpdateReplyVoteCommand(
    CommentId CommentId,
    ReplyId ReplyId,
    VoteId VoteId,
    UserId UserId,
    bool IsVoted)
: IRequest<ErrorOr<UpdateReplyVoteResult>>;