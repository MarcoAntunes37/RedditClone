namespace RedditClone.Application.Post.Commands.UpdatePost;

using MediatR;
using RedditClone.Application.Post.Results.UpdatePostResult;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record UpdatePostCommand(
    PostId PostId,
    UserId UserId,
    string Title,
    string Content)
: IRequest<UpdatePostResult>;