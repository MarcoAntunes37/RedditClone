namespace RedditClone.Application.Post.Commands.UpdateVoteOnPost;

using MediatR;
using RedditClone.Application.Post.Results.UpdateVoteOnPostResult;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record UpdateVoteOnPostCommand(
    VoteId VoteId,
    PostId PostId,
    UserId UserId,
    bool IsVoted)
: IRequest<UpdateVoteOnPostResult>;