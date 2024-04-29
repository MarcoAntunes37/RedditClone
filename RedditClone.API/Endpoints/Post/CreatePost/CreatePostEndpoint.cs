
using ErrorOr;
using MediatR;
using RedditClone.API.Extension;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Application.Post.Commands.CreatePost;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Application.Post.Results.CreatePostResult;

namespace RedditClone.API.Endpoints.Post.CreatePost;

public class CreatePostEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/posts/create", async (
            CreatePostRequest request,
            ISender mediator) =>
        {
            var command = new CreatePostCommand(
                new CommunityId(request!.CommunityId),
                new UserId(request!.UserId),
                request.Title,
                request.Content,
                new());

            ErrorOr<CreatePostResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => ProblemExtensions.CreateProblemDetails(errors));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Posts)
        .RequireAuthorization();
    }
}