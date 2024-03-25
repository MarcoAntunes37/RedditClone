namespace RedditClone.Application.Community.Commands.UpdateCommunity;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Community.Results.UpdateCommunityResult;
using RedditClone.Application.Persistence;
using Serilog;

public class UpdateCommunityCommandHandler :
    IRequestHandler<UpdateCommunityCommand, UpdateCommunityResult>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IValidator<UpdateCommunityCommand> _validator;
    private readonly IConfiguration _configuration;

    public UpdateCommunityCommandHandler(
        ICommunityRepository communityRepository,
        IValidator<UpdateCommunityCommand> validator,
        IConfiguration configuration)
    {
        _communityRepository = communityRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<UpdateCommunityResult> Handle(UpdateCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "");

        _validator.ValidateAndThrow(command);

        _communityRepository.UpdateCommunityById(command.CommunityId, command.UserId, command.Name, command.Description, command.Topic);

        var community = _communityRepository.GetCommunityById(command.CommunityId);

        UpdateCommunityResult result = new("Community successfully updated.", community);

        Log.Information(
            "{@UpdateCommunityResult}",
            result);

        return result;
    }
}