namespace RedditClone.API.Endpoints.ReplyVotes.UpdateReplyVote;

using ErrorOr;
using MediatR;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.ReplyVotes.Commands.UpdateReplyVote;
using RedditClone.API.Endpoints.ReplyVotes.UpdateUpdateReplyVoteVote;
using RedditClone.Application.ReplyVotes.Results.UpdateReplyVoteResult;
using RedditClone.API.Extension;

public class UpdateReplyVoteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/replies/{commentId}/{replyId}/update-vote/{voteId}", async (
            Guid replyId,
            Guid commentId,
            Guid voteId,
            UpdateReplyVoteRequest req,
            ISender mediator
        ) =>
        {
            var command = new UpdateReplyVoteCommand(
                new CommentId(commentId),
                new ReplyId(replyId),
                new VoteId(voteId),
                new UserId(req.UserId),
                req.IsVoted);

            ErrorOr<UpdateReplyVoteResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.ReplyVotes)
        .RequireAuthorization();
    }
}