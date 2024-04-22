namespace RedditClone.Application.PostVotes.Commands.UpdateVote;

using ErrorOr;
using MediatR;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.PostVotes.Results.UpdatePostVoteResult;

public record UpdatePostVoteCommand(
    VoteId VoteId,
    PostId PostId,
    UserId UserId,
    bool IsVoted)
: IRequest<ErrorOr<UpdatePostVoteResult>>;