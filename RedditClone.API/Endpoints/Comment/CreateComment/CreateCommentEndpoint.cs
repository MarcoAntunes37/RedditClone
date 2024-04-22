namespace RedditClone.API.Endpoints.Comment.CreateComment;

using ErrorOr;
using MediatR;
using RedditClone.Application.Comment.Commands.CreateComment;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;


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
                errors => Results.Problem(
                    errors.First().Code,
                    errors.First().Description));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Comments);
    }
}