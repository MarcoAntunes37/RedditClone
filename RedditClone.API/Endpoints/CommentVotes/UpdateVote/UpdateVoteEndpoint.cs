namespace RedditClone.API.Endpoints.CommentVotes.UpdateVote;

using MediatR;
using ErrorOr;
using RedditClone.API.Extension;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentVotes.Commands.UpdateCommentVote;
using RedditClone.Application.CommentVotes.Results.UpdateCommentVoteResult;

public class UpdateVoteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/comments/{commentId}/update-vote/{voteId}", async (
            Guid commentId,
            Guid voteId,
            UpdateVoteRequest req,
            ISender mediator) =>
        {
            var command = new UpdateCommentVoteCommand(
                new CommentId(commentId),
                new VoteId(voteId),
                new UserId(req.UserId),
                req.IsVoted
            );

            ErrorOr<UpdateCommentVoteResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.CommentVotes)
        .RequireAuthorization();
    }
}