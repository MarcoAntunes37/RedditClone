namespace RedditClone.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.API.Mappers;
using RedditClone.Application.Community.Results.CreateCommunityResult;
using RedditClone.Application.Community.Results.DeleteCommunityResult;
using RedditClone.Application.Community.Results.TransferCommunityResult;
using RedditClone.Application.Community.Results.UpdateCommunityResult;
using RedditClone.Contracts.Community.CreateCommunity;
using RedditClone.Contracts.Community.DeleteCommunity;
using RedditClone.Contracts.Community.TransferCommunityOwnership;
using RedditClone.Contracts.Community.UpdateCommunity;

[Route("communities")]
public class CommunityController : ApiController
{
    private readonly ISender _sender;

    public CommunityController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("new-community/{ownerId}")]
    public async Task<IActionResult> CreateCommunity(
        [FromBody] CreateCommunityRequest request,
        [FromRoute] Guid ownerId)
    {
        var command = CommunityMappers.MapCreateCommunityRequest(request, ownerId);

        CreateCommunityResult result = await _sender.Send(command);

        return Ok(CommunityMappers.MapCreateCommunityResponse(result));
    }

    [HttpPut("update-community/{communityId}")]
    public async Task<IActionResult> UpdateCommunity(
        [FromBody] UpdateCommunityRequest request,
        [FromRoute] Guid communityId)
    {
        var command = CommunityMappers.MapUpdateCommunityRequest(request, communityId);

        UpdateCommunityResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPut("update-community/transfer/{communityId}")]
    public async Task<IActionResult> UpdateCommunityOwnership(
        [FromBody] TransferCommunityOwnershipRequest request,
        [FromRoute] Guid communityId)
{
        var command = CommunityMappers.MapTransferCommunity(communityId, request);

        TransferCommunityResult result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("delete-community/{communityId}")]
    public async Task<IActionResult> DeleteCommunity(
        [FromRoute] Guid communityId,
        [FromBody] DeleteCommunityRequest request)
    {
        var command = CommunityMappers.MapDeleteCommunityRequest(communityId, request);

        DeleteCommunityResult result = await _sender.Send(command);

        return Ok(result);
    }
}