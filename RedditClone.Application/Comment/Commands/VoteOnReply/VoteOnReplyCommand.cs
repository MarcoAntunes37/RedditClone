namespace RedditClone.Application.Comment.Commands.VoteOnReply;

using MediatR;
using RedditClone.Application.Comment.Results.VoteOnReplyResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record VoteOnReplyCommand(
    CommentId CommentId,
    ReplyId ReplyId,
    UserId UserId,
    bool IsVoted)
: IRequest<VoteOnReplyResult>;