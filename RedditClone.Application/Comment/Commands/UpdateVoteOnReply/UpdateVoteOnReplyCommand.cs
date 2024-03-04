namespace RedditClone.Application.Comment.Commands.UpdateVoteOnReply;

using MediatR;
using RedditClone.Application.Comment.Results.UpdateVoteOnReplyResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record UpdateVoteOnReplyCommand(
    CommentId CommentId,
    ReplyId ReplyId,
    VoteId VoteId,
    UserId UserId,
    bool IsVoted)
: IRequest<UpdateVoteOnReplyResult>;