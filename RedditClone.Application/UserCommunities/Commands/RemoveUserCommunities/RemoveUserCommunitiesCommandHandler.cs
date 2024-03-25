namespace RedditClone.Application.UserCommunities.Commands.RemoveUserCommunities;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Results.AddUserCommunitiesResults;
using Serilog;

public class RemoveUserCommunitiesCommandHandler
    : IRequestHandler<RemoveUserCommunitiesCommand, RemoveUserCommunitiesResult>
{
    private readonly IUserCommunitiesRepository _userCommunityRepository;
    private readonly IValidator<RemoveUserCommunitiesCommand> _validator;
    private readonly IConfiguration _configuration;

    public RemoveUserCommunitiesCommandHandler(
        IUserCommunitiesRepository userCommunityRepository,
        IValidator<RemoveUserCommunitiesCommand> validator,
        IConfiguration configuration)
    {
        _userCommunityRepository = userCommunityRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<RemoveUserCommunitiesResult> Handle(RemoveUserCommunitiesCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@RemoveUserCommunitiesCommand}",
            "Trying to remove User: {@UserId} from Community: {@CommunityId}",
            command,
            command.UserId,
            command.CommunityId);

        _validator.ValidateAndThrow(command);

        _userCommunityRepository.Remove(command.UserId, command.CommunityId);

        RemoveUserCommunitiesResult result = new ("User left community successfully");

        Log.Information(
            "{@RemoveUserCommunitiesResult}, User: {@UserId}, Community: {@CommunityId}",
            result,
            command.UserId,
            command.CommunityId);

        return result;
    }
}