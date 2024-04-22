namespace RedditClone.API.Endpoints.PostVotes.DeletePostVote;

using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.PostVotes.Commands.DeleteVote;
using RedditClone.Application.Post.Results.DeletePostVoteResult;

public class DeletePostVoteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/posts/{postId}/delete-vote/{voteId}", async (
            Guid postId,
            Guid voteId,
            [FromBody] DeletePostVoteRequest req,
            ISender mediator
        ) =>
        {
            var command = new DeletePostVoteCommand(
                new VoteId(voteId),
                new PostId(postId),
                new UserId(req.UserId));

            ErrorOr<DeletePostVoteResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => Results.Problem(
                    errors.First().Code,
                    errors.First().Description));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.PostVotes);
    }
}