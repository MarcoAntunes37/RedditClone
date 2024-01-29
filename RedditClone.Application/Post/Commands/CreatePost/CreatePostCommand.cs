using ErrorOr;
using MediatR;
using RedditClone.Domain.PostAggregate;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;

namespace RedditClone.Application.Post.Commands.CreatePost;

public record CreatePostCommand(
    string Title,
    string Content,
    UserId UserId,
    CommunityId CommunityId,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    List<Upvotes> Upvotes,
    List<Downvotes> Downvotes) : IRequest<ErrorOr<PostAggregate>>;