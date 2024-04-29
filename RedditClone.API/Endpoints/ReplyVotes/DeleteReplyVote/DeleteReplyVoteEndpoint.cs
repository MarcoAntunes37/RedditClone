namespace RedditClone.API.Endpoints.ReplyVotes.DeleteReplyVote;

using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.API.Extension;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.ReplyVotes.Commands.DeleteReplyVote;
using RedditClone.Application.ReplyVotes.Results.DeleteReplyVoteResult;

public class DeleteReplyVoteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/replies/{commentId}/{replyId}/delete-vote/{voteId}", async (
            Guid commentId,
            Guid replyId,
            Guid voteId,
            [FromBody] DeleteReplyVoteRequest req,
            ISender mediator) => {

            var command = new DeleteReplyVoteCommand(
                new CommentId(commentId),
                new ReplyId(replyId),
                new VoteId(voteId),
                new UserId(req.UserId));

            ErrorOr<DeleteReplyVoteResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.ReplyVotes)
        .RequireAuthorization();
    }
}