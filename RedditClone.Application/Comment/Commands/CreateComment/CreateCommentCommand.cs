namespace RedditClone.Application.Comment.Commands.CreateComment;

using MediatR;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record CreateCommentCommand(
    UserId UserId,
    CommunityId CommunityId,
    PostId PostId,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<Votes> Votes,
    List<Replies> Replies)
: IRequest<CreateCommentResult>;