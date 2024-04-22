namespace RedditClone.Application.Post.Commands.CreatePost;

using ErrorOr;
using MediatR;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Post.Results.CreatePostResult;

public record CreatePostCommand(
    CommunityId CommunityId,
    UserId UserId,
    string Title,
    string Content,
    List<Votes> Votes)
: IRequest<ErrorOr<CreatePostResult>>;