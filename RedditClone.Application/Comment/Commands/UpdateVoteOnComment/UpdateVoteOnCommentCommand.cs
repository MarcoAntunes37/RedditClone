namespace RedditClone.Application.Comment.Commands.UpdateVoteOnComment;

using MediatR;
using RedditClone.Application.Community.Results.UpdateVoteOnCommentResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record UpdateVoteOnCommentCommand(
    CommentId CommentId,
    VoteId VoteId,
    UserId UserId,
    bool IsVoted
) : IRequest<UpdateVoteOnCommentResult>;