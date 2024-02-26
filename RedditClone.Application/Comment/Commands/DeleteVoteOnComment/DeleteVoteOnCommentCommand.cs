namespace RedditClone.Application.Community.Commands.DeleteVoteOnComment;

using MediatR;
using RedditClone.Application.Community.Results.DeleteVoteOnCommentResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record DeleteVoteOnCommentCommand(
    CommentId CommentId,
    VoteId VoteId,
    UserId UserId
) : IRequest<DeleteVoteOnCommentResult>;