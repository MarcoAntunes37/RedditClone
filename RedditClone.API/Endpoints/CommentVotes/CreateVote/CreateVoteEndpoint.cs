namespace RedditClone.API.Endpoints.CommentVotes.CreateVote;

using ErrorOr;
using MediatR;
using RedditClone.Application.CommentVotes.Commands.CreateCommentVote;
using RedditClone.Application.CommentVotes.Results.CreateCommentVoteResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

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
                errors => Results.Problem(
                    errors.First().Code,
                    errors.First().Description));

        }).MapToApiVersion(1)
        .WithTags(Tags.CommentVotes);
    }
}