namespace RedditClone.Application.Comment.Commands.VoteOnComment;

using MediatR;
using RedditClone.Application.Comment.Results.VoteOnCommentResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record VoteOnCommentCommand(
    CommentId CommentId,
    UserId UserId,
    bool IsVoted)
: IRequest<VoteOnCommentResult>;