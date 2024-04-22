namespace RedditClone.Application.CommentVotes.Commands.DeleteCommentVote;

using ErrorOr;
using MediatR;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentVotes.Results.DeleteCommentVoteResult;

public record DeleteCommentVoteCommand(
    CommentId CommentId,
    VoteId VoteId,
    UserId UserId
) : IRequest<ErrorOr<DeleteCommentVoteResult>>;