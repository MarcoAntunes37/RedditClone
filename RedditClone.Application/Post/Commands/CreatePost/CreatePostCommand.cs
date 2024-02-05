using MediatR;
using RedditClone.Application.Post.Results;
using RedditClone.Domain.PostAggregate.ValueObjects;

namespace RedditClone.Application.Post.Commands.CreatePost;

public record CreatePostCommand(
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt) : IRequest<CreatePostResult>;