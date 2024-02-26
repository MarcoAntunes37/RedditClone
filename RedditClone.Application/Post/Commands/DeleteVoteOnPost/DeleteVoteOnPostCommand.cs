namespace RedditClone.Application.Post.Commands.DeleteVoteOnPost;

using MediatR;
using RedditClone.Application.Post.Results.DeleteVoteOnPostResult;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record DeleteVoteOnPostCommand(
    VoteId VoteId,
    PostId PostId,
    UserId UserId)
: IRequest<DeleteVoteOnPostResult>;