namespace RedditClone.API.Endpoints.CommentVotes.CreateVote;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentVotes.Commands.CreateCommentVote;
using RedditClone.Application.CommentVotes.Results.CreateCommentVoteResult;

public class CreateVoteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/comments/{commentId}/create-vote", async (
            Guid commentId,
            CreateVoteRequest req,
            ISender mediator) =>
        {
            var command = new CreateCommentVoteCommand(
                new CommentId(commentId),
                new UserId(req.UserId),
                req.IsVoted);

            ErrorOr<CreateCommentVoteResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));

        })
        .MapToApiVersion(1)
        .WithTags(Tags.CommentVotes)
        .RequireAuthorization();
    }
}