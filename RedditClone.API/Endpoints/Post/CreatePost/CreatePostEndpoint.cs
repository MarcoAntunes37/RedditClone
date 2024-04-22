
using ErrorOr;
using MediatR;
using RedditClone.Application.Post.Commands.CreatePost;
using RedditClone.Application.Post.Results.CreatePostResult;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

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
                errors => Results.Problem(
                    errors.First().Code,
                    errors.First().Description));
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Posts);
    }
}