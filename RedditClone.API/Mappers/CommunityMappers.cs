using RedditClone.Application.Community.Commands.CreateCommunity;
using RedditClone.Application.Community.Commands.DeleteCommunity;
using RedditClone.Application.Community.Commands.TransferCommunity;
using RedditClone.Application.Community.Commands.UpdateCommunity;
using RedditClone.Application.Community.Results.CreateCommunityResult;
using RedditClone.Contracts.Community.CreateCommunity;
using RedditClone.Contracts.Community.DeleteCommunity;
using RedditClone.Contracts.Community.TransferCommunityOwnership;
using RedditClone.Contracts.Community.UpdateCommunity;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

namespace RedditClone.API.Mappers;

public class CommunityMappers
{
    public static CreateCommunityCommand MapCreateCommunityRequest(
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

    public static CreateCommunityResponse MapCreateCommunityResponse(
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

    public static UpdateCommunityCommand MapUpdateCommunityRequest(
            UpdateCommunityRequest request,
            Guid communityId)
    {
        return new UpdateCommunityCommand(
            CommunityId.Create(communityId),
            UserId.Create(request.UserId),
            request.Name,
            request.Description,
            request.Topic
        );
    }

    public static DeleteCommunityCommand MapDeleteCommunityRequest(Guid communityId, DeleteCommunityRequest request)
    {
        return new DeleteCommunityCommand(
            CommunityId.Create(communityId),
            UserId.Create(request.UserId)
        );
    }

    public static TransferCommunityCommand MapTransferCommunity(Guid communityId, TransferCommunityOwnershipRequest request)
    {
        return new TransferCommunityCommand(
            UserId.Create(request.OwnerId),
            UserId.Create(request.NewOwnerId),
            CommunityId.Create(communityId)
        );
    }
}