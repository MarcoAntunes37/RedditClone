namespace RedditClone.API.Mappers;

using RedditClone.Application.Community.Commands.CreateCommunity;
using RedditClone.Application.Community.Commands.DeleteCommunity;
using RedditClone.Application.Community.Commands.TransferCommunity;
using RedditClone.Application.Community.Commands.UpdateCommunity;
using RedditClone.Application.Community.Results.CreateCommunityResult;
using RedditClone.Contracts.Community.DeleteCommunity;
using RedditClone.Contracts.Community.TransferCommunityOwnership;
using RedditClone.Contracts.Community.UpdateCommunity;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Contracts.Community.CreateCommunity;
using RedditClone.Application.Community.Results.UpdateCommunityResult;
using RedditClone.Application.Community.Queries.GetCommunitiesById;
using RedditClone.Application.Community.Queries.GetCommunitiesList;

public class CommunityMappers
{
    public static GetCommunityByIdQuery MapGetCommunityByIdRequest(Guid communityId)
    {
        return new GetCommunityByIdQuery(
            new CommunityId(communityId)
        );
    }

    public static GetCommunitiesListQuery MapGetCommunitiesListRequest(string name, string topic, int page, int pageSize)
    {
        return new GetCommunitiesListQuery(
            name,
            topic,
            page,
            pageSize
        );
    }

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
            new UserId(ownerId)
        );
    }

    public static CreateCommunityResponse MapCreateCommunityResponse(
        CreateCommunityResult result)
    {
        var community = result.Community;
        return new CreateCommunityResponse(
            result.Message,
            community.Name,
            community.Description,
            community.Topic,
            community.CreatedAt,
            community.UpdatedAt
        );
    }

    public static UpdateCommunityCommand MapUpdateCommunityRequest(
            UpdateCommunityRequest request,
            Guid communityId)
    {
        return new UpdateCommunityCommand(
            new CommunityId(communityId),
            new UserId(request.UserId),
            request.Name,
            request.Description,
            request.Topic
        );
    }

    public static UpdateCommunityResponse MapUpdateCommunityResponse(
        UpdateCommunityResult result)
    {
        var community = result.Community;
        return new UpdateCommunityResponse(
            result.Message,
            community.Name,
            community.Description,
            community.Topic
        );
    }

    public static DeleteCommunityCommand MapDeleteCommunityRequest(
        Guid communityId,
        DeleteCommunityRequest request)
    {
        return new DeleteCommunityCommand(
            new CommunityId(communityId),
            new UserId(request.UserId)
        );
    }

    public static TransferCommunityCommand MapTransferCommunity(
        Guid communityId,
        TransferCommunityOwnershipRequest request)
    {
        return new TransferCommunityCommand(
            new UserId(request.OwnerId),
            new UserId(request.NewOwnerId),
            new CommunityId(communityId)
        );
    }
}