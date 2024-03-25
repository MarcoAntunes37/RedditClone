namespace RedditClone.Application.Community.Commands.CreateCommunity;

using System.Net;
using FluentValidation;
using MediatR;
using RedditClone.Application.Community.Results.CreateCommunityResult;
using RedditClone.Application.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Common.Helpers;
using Serilog;

public class CreateCommunityCommandHandler :
    IRequestHandler<CreateCommunityCommand, CreateCommunityResult>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IValidator<CreateCommunityCommand> _validator;
    private readonly IConfiguration _configuration;

    public CreateCommunityCommandHandler(
        ICommunityRepository communityRepository,
        IValidator<CreateCommunityCommand> validator,
        IConfiguration configuration)
    {
        _communityRepository = communityRepository;
        _validator = validator;
        _configuration = configuration;
    }

    public async Task<CreateCommunityResult> Handle(CreateCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@CreateCommunityCommand}",
            "Trying to create Community with Owner: {@UserId}",
            command.UserId);

        if(_communityRepository.GetCommunityByName(command.Name) is not null)
            throw new HttpCustomException(HttpStatusCode.Conflict, $"Community {command.Name} already exists");

        _validator.ValidateAndThrow(command);

        var community = Community.Create(
            command.UserId,
            command.Name,
            command.Description,
            command.Topic,
            command.CreatedAt,
            command.UpdatedAt
        );

        _communityRepository.Add(community);

        CreateCommunityResult result = new(community);

        Log.Information(
            "{@Message}", "{@CreateCommunityResult}",
            "User created successfully",
            result);

        return result;
    }
}