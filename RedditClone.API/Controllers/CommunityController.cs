using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Community.Commands.CreateCommunity;
using RedditClone.Application.Community.Results.CreateCommunityResult;
using RedditClone.Contracts.Community;

namespace RedditClone.API.Controllers;

[Route("communities/new-community")]
public class CommunityController : ApiController
{
    private readonly ISender _sender;

    public CommunityController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCommunity(
        [FromBody] CreateCommunityRequest request)
    {
        var command = MapCreateCommunityCommand(request);

        CreateCommunityResult result = await _sender.Send(command);

        return Ok(MapCreateCommunityResponse(result));
    }

    private static CreateCommunityCommand MapCreateCommunityCommand(
        CreateCommunityRequest request
        )
    {
        return new CreateCommunityCommand(
            request.Name,
            request.Description,
            request.Topic,
            DateTime.Now,
            DateTime.Now
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
            community.UpdatedAt
        );
    }
}