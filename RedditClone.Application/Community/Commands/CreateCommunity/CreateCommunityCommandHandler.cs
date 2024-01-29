using ErrorOr;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate;

namespace RedditClone.Application.Community.Commands.CreateCommunity;

public class CreateCommunityCommandHandler :
    IRequestHandler<CreateCommunityCommand,
    ErrorOr<CommunityAggregate>>
{
    private readonly ICommunityRepository _communityRepository;

    public CreateCommunityCommandHandler(ICommunityRepository communityRepository)
    {
        _communityRepository = communityRepository;
    }

    public async Task<ErrorOr<CommunityAggregate>> Handle(CreateCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        //create community
        var community = CommunityAggregate.Create(
            command.UserId,
            command.Name,
            command.Description,
            command.MembersCount,
            command.Topic,
            command.CreatedAt,
            command.UpdatedAt
        );

        //persist community
        _communityRepository.Add(community);

        //return community
        return community;
    }
}