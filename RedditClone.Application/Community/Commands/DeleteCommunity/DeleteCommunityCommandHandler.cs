namespace RedditClone.Application.Community.Commands.DeleteCommunity;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using RedditClone.Application.Community.Results.DeleteCommunityResult;
using RedditClone.Application.Persistence;
using Serilog;

public class DeleteCommunityCommandHandler :
    IRequestHandler<DeleteCommunityCommand, DeleteCommunityResult>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IValidator<DeleteCommunityCommand> _validator;
    private readonly IConfiguration _configuration;

    public DeleteCommunityCommandHandler(
        ICommunityRepository communityRepository,
        IValidator<DeleteCommunityCommand> validator,
        IConfiguration configuration)
    {
        _communityRepository = communityRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<DeleteCommunityResult> Handle(DeleteCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@DeleteCommunityCommand}",
            "Trying to delete Community: {@CommunityId} with Owner: {@UserId}",
            command,
            command.CommunityId,
            command.UserId);

        _validator.ValidateAndThrow(command);

        _communityRepository.DeleteCommunityById(command.CommunityId, command.UserId);

        DeleteCommunityResult result = new("Community successfully Deleted.");

        Log.Information(
            "{@DeleteCommunityResult}, {@CommunityId}",
            result,
            command.CommunityId);

        return result;
    }
}