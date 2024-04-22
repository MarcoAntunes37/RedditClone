namespace RedditClone.Application.CommentReplies.Commands.CreateCommentReply;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.CommentReplies.Results.CreateCommentReplyResults;

public record CreateCommentReplyCommand(
    UserId UserId,
    CommunityId CommunityId,
    CommentId CommentId,
    string Content)
: IRequest<ErrorOr<CreateCommentReplyResult>>;