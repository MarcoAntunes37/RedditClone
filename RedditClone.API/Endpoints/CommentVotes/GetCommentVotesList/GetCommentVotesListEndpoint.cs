namespace RedditClone.API.Endpoints.CommentVotes.GetCommentVotesList;

using MediatR;
using RedditClone.Application.CommentVotes.Queries;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentVotes.Results.GetCommentVotesListResult;

public class GetCommentVotesListEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/comments/{commentId}/votes", async (
            Guid commentId,
            ISender mediator) =>
        {
            var query = new GetCommentVotesListQuery(
                new CommentId(commentId));

            GetCommentVotesListResult result = await mediator.Send(query);

            return Results.Ok(result);
        })
        .MapToApiVersion(1)
        .WithTags(Tags.CommentVotes)
        .RequireAuthorization();
    }
}