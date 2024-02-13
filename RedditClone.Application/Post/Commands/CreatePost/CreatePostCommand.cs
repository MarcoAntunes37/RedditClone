namespace RedditClone.Application.Post.Commands.CreatePost;

using MediatR;
using RedditClone.Application.Post.Results;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;

public record CreatePostCommand(
    CommunityId CommunityId,
    UserId UserId,
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<Votes> Votes)
: IRequest<CreatePostResult>;