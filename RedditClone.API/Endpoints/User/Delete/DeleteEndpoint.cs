namespace RedditClone.API.Endpoints.User.Delete;

using ErrorOr;
using MediatR;
using RedditClone.Application.User.Results;
using RedditClone.Application.User.Commands.Delete;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class DeleteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/users/{currentUserId}/delete/{userId}", async (
            Guid userId,
            Guid currentUserId,
            ISender mediator) =>
        {
            var command = new DeleteUserCommand(
                new UserId(userId),
                new UserId(currentUserId));

            ErrorOr<DeleteResult> result = await mediator.Send(command);

            return result.Match(
                result => Results.Ok(result),
                errors => Results.Problem(result.FirstError.Code, result.FirstError.Description)
            );
        })
        .MapToApiVersion(1)
        .WithTags(Tags.Users)
        .RequireAuthorization();
    }
}