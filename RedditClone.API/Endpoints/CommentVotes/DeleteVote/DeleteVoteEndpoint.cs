namespace RedditClone.API.Endpoints.CommentVotes.DeleteVote;

using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.API.Extension;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentVotes.Commands.DeleteCommentVote;
using RedditClone.Application.CommentVotes.Results.DeleteCommentVoteResult;

public class DeleteVoteEndpoint : IEndpoint
{
    public DeleteVoteEndpoint()
    {
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/comments/{commentId}/delete-vote/{voteId}", async (
            Guid commentId,
            Guid voteId,
            [FromBody] DeleteVoteRequest req,
            ISender mediator) =>
        {
            var command = new DeleteCommentVoteCommand(
                new CommentId(commentId),
                new VoteId(voteId),
                new UserId(req.UserId));

            ErrorOr<DeleteCommentVoteResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.CommentVotes)
        .RequireAuthorization();
    }
}