namespace RedditClone.API.Endpoints.CommentVotes.DeleteVote;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.API.Endpoints.CommentReplies.CreateReply;
using RedditClone.Application.CommentReplies.Commands.CreateCommentReply;
using RedditClone.Application.CommentReplies.Results.CreateCommentReplyResults;

public class CreateReplyEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/comments/{commentId}/replies", async (
            Guid commentId,
            CreateReplyRequest req,
            ISender mediator) =>
        {
            var command = new CreateCommentReplyCommand(
                new UserId(req.UserId),
                new CommunityId(req.CommunityId),
                new CommentId(commentId),
                req.Content);

            ErrorOr<CreateCommentReplyResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.CommentReplies)
        .RequireAuthorization();
    }
}