namespace RedditClone.API.Endpoints.Post.DeletePost;

using ErrorOr;
using MediatR;
using RedditClone.Application.Post.Commands.DeletePost;
using RedditClone.Application.Post.Results.DeletePostResult;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class DeletePostEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/posts/{postId}/delete/{currentUserId}", async (
            Guid postId,
            Guid currentUserId,
            ISender mediator) =>
        {
            var command = new DeletePostCommand(
                new PostId(postId),
                new UserId(currentUserId));

            ErrorOr<DeletePostResult> result = await mediator.Send(command);

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