namespace RedditClone.API.Endpoints.PostVotes.GetVotesListByPostId;

using MediatR;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Application.PostVotes.Queries.GetPostVotesList;
using RedditClone.Application.PostVotes.Results.GetPostListByCommunityIdResult;

public class GetVotesListByPostIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/posts/{postId}/votes", async (
            Guid postId,
            int page,
            int pageSize,
            ISender mediator) =>
        {
            var query = new GetPostVotesListQuery(
                new PostId(postId),
                page,
                pageSize);

            GetPostVotesListResult result = await mediator.Send(query);

            return Results.Ok(result);
        })
        .MapToApiVersion(1)
        .WithTags(Tags.PostVotes);
    }
}