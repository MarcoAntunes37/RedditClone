namespace RedditClone.Application.Community.Commands.DeleteVoteOnReply;

using MediatR;
using RedditClone.Application.Community.Results.DeleteVoteOnReplyResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record DeleteVoteOnReplyCommand(
    CommentId CommentId,
    ReplyId ReplyId,
    VoteId VoteId,
    UserId UserId)
: IRequest<DeleteVoteOnReplyResult>;