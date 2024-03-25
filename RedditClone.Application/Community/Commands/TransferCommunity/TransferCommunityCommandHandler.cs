namespace RedditClone.Application.Community.Commands.TransferCommunity;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Community.Results.TransferCommunityResult;
using RedditClone.Application.Persistence;
using Serilog;

public class TransferCommunityCommandHandler :
    IRequestHandler<TransferCommunityCommand, TransferCommunityResult>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IValidator<TransferCommunityCommand> _validator;
    private readonly IConfiguration _configuration;

    public TransferCommunityCommandHandler(
        ICommunityRepository communityRepository,
        IValidator<TransferCommunityCommand> validator,
        IConfiguration configuration)
    {
        _communityRepository = communityRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<TransferCommunityResult> Handle(TransferCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@TransferCommunityCommand}",
            "Trying to transfer Community: {@CommunityId} from User: {@UserId} to User: {@UserId}",
            command,
            command.UserId,
            command.NewUserId);

        _validator.ValidateAndThrow(command);

        _communityRepository.TransferCommunityOwnership(command.UserId, command.NewUserId, command.CommunityId);

        TransferCommunityResult result = new("Community successfully transferred.");

        Log.Information(
            "{@TransferCommunityResult}, {@CommunityId}",
            result,
            command.CommunityId);

        return result;
    }
}