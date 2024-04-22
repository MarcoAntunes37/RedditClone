namespace RedditClone.Application.Post.Commands.DeletePost;

using ErrorOr;
using MediatR;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.Post.Results.DeletePostResult;

public record DeletePostCommand(
    PostId PostId,
    UserId UserId)
: IRequest<ErrorOr<DeletePostResult>>;