namespace RedditClone.API.Endpoints.ReplyVotes.CreateReplyVote;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.ReplyVotes.Commands.CreateReplyVote;
using RedditClone.Application.ReplyVotes.Results.CreateReplyVoteResult;

public class CreateReplyVoteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/replies/{commentId}/{replyId}/create-vote", async (
            Guid commentId,
            Guid replyId,
            CreateReplyVoteRequest req,
            ISender mediator) =>
        {
            var command = new CreateReplyVoteCommand(
                new CommentId(commentId),
                new ReplyId(replyId),
                new UserId(req.UserId),
                req.IsVoted);

            ErrorOr<CreateReplyVoteResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.ReplyVotes)
        .RequireAuthorization();
    }
}