namespace RedditClone.Application.CommentVotes.Commands.UpdateCommentVote;

using ErrorOr;
using MediatR;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentVotes.Results.UpdateCommentVoteResult;

public record UpdateCommentVoteCommand(
    CommentId CommentId,
    VoteId VoteId,
    UserId UserId,
    bool IsVoted
) : IRequest<ErrorOr<UpdateCommentVoteResult>>;