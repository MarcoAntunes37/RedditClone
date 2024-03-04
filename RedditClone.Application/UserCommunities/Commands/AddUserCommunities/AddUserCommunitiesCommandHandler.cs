namespace RedditClone.Application.UserCommunities.Commands.AddUserCommunities;

using FluentValidation;
using MediatR;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Results.AddUserCommunitiesResults;
using RedditClone.Domain.UserCommunitiesAggregate;

public class AddUserCommunitiesCommandHandler
    : IRequestHandler<AddUserCommunitiesCommand, AddUserCommunitiesResult>
{
    private readonly IUserCommunitiesRepository _userCommunityRepository;
    private readonly IValidator<AddUserCommunitiesCommand> _validator;

    public AddUserCommunitiesCommandHandler(
        IUserCommunitiesRepository userCommunityRepository,
        IValidator<AddUserCommunitiesCommand> validator)
    {
        _userCommunityRepository = userCommunityRepository;
        _validator = validator;
    }

    public async Task<AddUserCommunitiesResult> Handle(AddUserCommunitiesCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        var userCommunities = UserCommunities.Create(
            command.UserId,
            command.CommunityId
        );

        _userCommunityRepository.Add(userCommunities);

        return new AddUserCommunitiesResult(
            "User joined in community successfully"
        );
    }
}