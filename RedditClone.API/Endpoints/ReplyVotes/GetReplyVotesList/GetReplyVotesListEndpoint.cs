namespace RedditClone.API.Endpoints.ReplyVotes.GetReplyVotesList;

using MediatR;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.ReplyVotes.Queries.GetRepliesVotesList;
using RedditClone.Application.ReplyVotes.Results.GetReplyVotesListResults;

public class GetReplyVotesListEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/replies/{replyId}/{commentId}/votes", async (
            Guid replyId,
            Guid commentId,
            ISender mediator) =>
        {
            var query = new GetReplyVotesListQuery(
                new CommentId(commentId),
                new ReplyId(replyId));

            GetReplyVotesListResult result = await mediator.Send(query);

            return Results.Ok(result);
        })
        .MapToApiVersion(1)
        .WithTags(Tags.ReplyVotes)
        .RequireAuthorization();
    }
}