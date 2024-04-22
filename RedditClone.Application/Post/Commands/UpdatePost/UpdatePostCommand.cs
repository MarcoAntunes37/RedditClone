namespace RedditClone.Application.Post.Commands.UpdatePost;

using ErrorOr;
using MediatR;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.Post.Results.UpdatePostResult;

public record UpdatePostCommand(
    PostId PostId,
    UserId UserId,
    string Title,
    string Content)
: IRequest<ErrorOr<UpdatePostResult>>;