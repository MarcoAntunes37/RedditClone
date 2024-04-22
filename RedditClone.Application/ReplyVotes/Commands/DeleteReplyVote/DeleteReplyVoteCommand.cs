namespace RedditClone.Application.ReplyVotes.Commands.DeleteReplyVote;

using ErrorOr;
using MediatR;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.ReplyVotes.Results.DeleteReplyVoteResult;

public record DeleteReplyVoteCommand(
    CommentId CommentId,
    ReplyId ReplyId,
    VoteId VoteId,
    UserId UserId)
: IRequest<ErrorOr<DeleteReplyVoteResult>>;