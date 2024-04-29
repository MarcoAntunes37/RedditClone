namespace RedditClone.API.Endpoints.Comment.CreateComment;

using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Comment.Commands.CreateComment;
using RedditClone.Application.Comment.Results.CreateCommentResult;


public class CreateCommentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/comments/create", async (
            CreateCommentRequest req,
            ISender mediator) =>
        {
            var command = new CreateCommentCommand(
                new UserId(req.UserId),
                new CommunityId(req.CommunityId),
                new PostId(req.PostId),
                req.Content,
                new(),
                new());

            ErrorOr<CreateCommentResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Comments)
        .RequireAuthorization();
    }
}