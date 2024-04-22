namespace RedditClone.Application.PostVotes.Commands.DeleteVote;

using ErrorOr;
using MediatR;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.Post.Results.DeletePostVoteResult;

public record DeletePostVoteCommand(
    VoteId VoteId,
    PostId PostId,
    UserId UserId)
: IRequest<ErrorOr<DeletePostVoteResult>>;