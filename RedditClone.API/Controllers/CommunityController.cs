using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedditClone.Application.Community.Commands.CreateCommunity;
using RedditClone.Contracts.Community;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

namespace RedditClone.API.Controllers;

[Route("communities/{userId}/new-community")]
public class CommunityController : ApiController
{
    private readonly ISender _sender;

    public CommunityController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCommunity(
        [FromBody]CreateCommunityRequest request, [FromRoute]string userId)
    {
        var command = MapCreateCommunityCommand(request, userId);

        var createCommunityResult = await _sender.Send(command);

        return createCommunityResult.Match(
            community => Ok(MapCreateCommunityResponse(community)),
            errors => Problem(errors)
        );
    }

    private static CreateCommunityCommand MapCreateCommunityCommand(
        CreateCommunityRequest request,
        string userId
        ){
        return new CreateCommunityCommand(
            UserId.Create(userId),
            request.Name,
            request.Description,
            request.MembersCount,
            request.Topic,
            DateTime.Now,
            DateTime.Now
        );
    }

    private static CreateCommunityResponse MapCreateCommunityResponse(
        CommunityAggregate community)
    {
        return new CreateCommunityResponse(
            community.Id.Value.ToString(),
            community.UserId.Value.ToString(),
            community.Name,
            community.Description,
            community.MembersCount,
            community.Topic,
            community.CreatedAt,
            community.UpdatedAt
        );
    }
}