namespace RedditClone.Application.Post.Commands.DeletePost;

using MediatR;
using RedditClone.Application.Post.Results.DeletePostResult;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record DeletePostCommand(
    PostId PostId,
    UserId UserId)
: IRequest<DeletePostResult>;