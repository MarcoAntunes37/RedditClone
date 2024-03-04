namespace RedditClone.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.API.Mappers;
using RedditClone.Application.UserCommunities.Results.AddUserCommunitiesResults;
using RedditClone.Contracts.AddUserCommunities;

[Route("user-communities")]
public class UserCommunitiesController : ApiController
{
    private readonly ISender _sender;

    public UserCommunitiesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("join-community")]
    public async Task<IActionResult> JoinCommunity(
        [FromBody] AddUserCommunitiesRequest request)
    {
        var command = UserCommunitiesMappers.MapAddUserCommunitiesRequest(request);

        AddUserCommunitiesResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("leave-community/{communityId}/{userId}")]
    public async Task<IActionResult> LeaveCommunity(
        [FromRoute] Guid userId,
        [FromRoute] Guid communityId)
    {
        var command = UserCommunitiesMappers.MapRemoveUserCommunitiesRequest(userId, communityId);

        RemoveUserCommunitiesResult result = await _sender.Send(command);

        return Ok(result);
    }
}