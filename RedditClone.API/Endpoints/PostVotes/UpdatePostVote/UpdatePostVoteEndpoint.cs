namespace RedditClone.API.Endpoints.PostVotes.UpdatePostVote;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.PostVotes.Commands.UpdateVote;
using RedditClone.Application.PostVotes.Results.UpdatePostVoteResult;

public class UpdatePostVoteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/posts/{postId}/update-vote/{voteId}", async (
            Guid postId,
            Guid voteId,
            UpdatePostVoteRequest req,
            ISender mediator) =>
        {

            var command = new UpdatePostVoteCommand(
                new VoteId(voteId),
                new PostId(postId),
                new UserId(req.UserId),
                req.IsVoted);

            ErrorOr<UpdatePostVoteResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.PostVotes)
        .RequireAuthorization();
    }
}