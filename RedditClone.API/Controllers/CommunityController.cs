namespace RedditClone.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.API.Mappers;
using RedditClone.Application.Community.Queries.GetCommunitiesById;
using RedditClone.Application.Community.Queries.GetCommunitiesList;
using RedditClone.Application.Community.Results.CreateCommunityResult;
using RedditClone.Application.Community.Results.DeleteCommunityResult;
using RedditClone.Application.Community.Results.GetCommunitiesListResult;
using RedditClone.Application.Community.Results.GetCommunityByIdResult;
using RedditClone.Application.Community.Results.TransferCommunityResult;
using RedditClone.Application.Community.Results.UpdateCommunityResult;
using RedditClone.Contracts.Community.CreateCommunity;
using RedditClone.Contracts.Community.DeleteCommunity;
using RedditClone.Contracts.Community.TransferCommunityOwnership;
using RedditClone.Contracts.Community.UpdateCommunity;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

[Route("communities")]
public class CommunityController : ApiController
{
    private readonly ISender _sender;

    public CommunityController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("list-communities")]
    public async Task<IActionResult> GetCommunitiesList(
        [FromQuery] string name = "",
        [FromQuery] string topic = "",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20
    )
    {
        var query = CommunityMappers.MapGetCommunitiesListRequest(name, topic, page, pageSize);

        GetCommunitiesListResult result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpGet("get-community/{communityId}")]
    public async Task<IActionResult> GetCommunitiesById(
        [FromRoute] Guid communityId
    )
    {
        var query = CommunityMappers.MapGetCommunityByIdRequest(communityId);

        GetCommunityByIdResult result = await _sender.Send(query);

        return Ok(result);
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

        return Ok(CommunityMappers.MapUpdateCommunityResponse(result));
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