namespace RedditClone.Application.Post.Commands.CreatePost;

using MediatR;
using RedditClone.Application.Post.Results.CreatePostResult;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record CreatePostCommand(
    CommunityId CommunityId,
    UserId UserId,
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<Votes> Votes)
: IRequest<CreatePostResult>;