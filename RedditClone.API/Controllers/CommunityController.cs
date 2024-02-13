using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Community.Commands.CreateCommunity;
using RedditClone.Application.Community.Results.CreateCommunityResult;
using RedditClone.Contracts.Community;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

namespace RedditClone.API.Controllers;

[Route("communities/{ownerId}/new-community")]
public class CommunityController : ApiController
{
    private readonly ISender _sender;

    public CommunityController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCommunity(
        [FromBody] CreateCommunityRequest request,
        [FromRoute] Guid ownerId)
    {
        var command = MapCreateCommunityCommand(request, ownerId);

        CreateCommunityResult result = await _sender.Send(command);

        return Ok(MapCreateCommunityResponse(result));
    }

    private static CreateCommunityCommand MapCreateCommunityCommand(
        CreateCommunityRequest request,
        Guid ownerId)
    {
        return new CreateCommunityCommand(
            request.Name,
            request.Description,
            request.Topic,
            DateTime.UtcNow,
            DateTime.UtcNow,
            UserId.Create(ownerId)
        );
    }

    private static CreateCommunityResponse MapCreateCommunityResponse(
        CreateCommunityResult result)
    {
        var community = result.Community;
        return new CreateCommunityResponse(
            community.Id.Value.ToString(),
            community.Name,
            community.Description,
            community.Topic,
            community.CreatedAt,
            community.UpdatedAt,
            community.UserId.Value.ToString()
        );
    }
}