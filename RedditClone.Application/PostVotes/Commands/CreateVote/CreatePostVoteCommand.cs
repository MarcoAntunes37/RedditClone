namespace RedditClone.Application.PostVotes.Commands.CreateVote;

using ErrorOr;
using MediatR;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.Post.Results.CreatePostVoteResult;

public record CreatePostVoteCommand(
    PostId PostId,
    UserId UserId,
    bool IsVoted)
: IRequest<ErrorOr<CreatePostVoteResult>>;