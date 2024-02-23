namespace RedditClone.Application.Post.Commands.VoteOnPost;

using MediatR;
using RedditClone.Application.Post.Results.VoteOnPostResult;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record VoteOnPostCommand(
    PostId PostId,
    UserId UserId,
    bool IsVoted)
: IRequest<VoteOnPostResult>;