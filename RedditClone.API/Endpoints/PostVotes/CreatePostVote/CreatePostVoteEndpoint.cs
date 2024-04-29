
namespace RedditClone.API.Endpoints.PostVotes.CreatePostVote;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.PostVotes.Commands.CreateVote;
using RedditClone.Application.Post.Results.CreatePostVoteResult;

public class CreatePostVoteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/posts/{postId}/create-vote", async (
            Guid postId,
            CreatePostVoteRequest req,
            ISender mediator) =>
        {
            var command = new CreatePostVoteCommand(
                new PostId(postId),
                new UserId(req.UserId),
                req.IsVoted);

            ErrorOr<CreatePostVoteResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.PostVotes)
        .RequireAuthorization();
    }
}