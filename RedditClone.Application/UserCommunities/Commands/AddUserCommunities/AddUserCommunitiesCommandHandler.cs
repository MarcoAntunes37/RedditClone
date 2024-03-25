namespace RedditClone.Application.UserCommunities.Commands.AddUserCommunities;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Results.AddUserCommunitiesResults;
using RedditClone.Domain.UserCommunitiesAggregate;
using Serilog;

public class AddUserCommunitiesCommandHandler
    : IRequestHandler<AddUserCommunitiesCommand, AddUserCommunitiesResult>
{
    private readonly IUserCommunitiesRepository _userCommunityRepository;
    private readonly IValidator<AddUserCommunitiesCommand> _validator;
    private readonly IConfiguration _configuration;

    public AddUserCommunitiesCommandHandler(
        IUserCommunitiesRepository userCommunityRepository,
        IValidator<AddUserCommunitiesCommand> validator,
        IConfiguration configuration)
    {
        _userCommunityRepository = userCommunityRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<AddUserCommunitiesResult> Handle(AddUserCommunitiesCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@AddUserCommunitiesCommand}",
            "Trying to add User: {@UserId} to community: {@CommunityId}",
            command,
            command.UserId,
            command.CommunityId);

        _validator.ValidateAndThrow(command);

        var userCommunities = UserCommunities.Create(
            command.UserId,
            command.CommunityId
        );

        _userCommunityRepository.Add(userCommunities);

        AddUserCommunitiesResult result = new("User joined in community successfully");

        Log.Information(
            "{@AddUserCommunitiesResult}, UserId: {@UserId}, CommunityId: {@CommunityId}",
            result,
            command.UserId,
            command.CommunityId);

        return result;
    }
}