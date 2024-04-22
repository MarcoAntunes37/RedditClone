namespace RedditClone.API.Endpoints.CommentVotes.UpdateVote;

using MediatR;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentVotes.Commands.UpdateCommentVote;
using RedditClone.Application.CommentVotes.Results.UpdateCommentVoteResult;
using ErrorOr;

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
                errors => Results.Problem(
                    errors.First().Code,
                    errors.First().Description));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.CommentVotes);
    }
}