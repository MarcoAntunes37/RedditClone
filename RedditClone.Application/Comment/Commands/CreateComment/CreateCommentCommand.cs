namespace RedditClone.Application.Comment.Commands.CreateComment;

using ErrorOr;
using MediatR;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Comment.Results.CreateCommentResult;

public record CreateCommentCommand(
    UserId UserId,
    CommunityId CommunityId,
    PostId PostId,
    string Content,
    List<Votes> Votes,
    List<Replies> Replies)
: IRequest<ErrorOr<CreateCommentResult>>;