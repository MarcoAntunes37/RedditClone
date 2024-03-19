namespace RedditClone.Application.Community.Commands.CreateCommunity;

using System.Net;
using FluentValidation;
using MediatR;
using RedditClone.Application.Community.Results.CreateCommunityResult;
using RedditClone.Application.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate;

public class CreateCommunityCommandHandler :
    IRequestHandler<CreateCommunityCommand, CreateCommunityResult>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IValidator<CreateCommunityCommand> _validator;

    public CreateCommunityCommandHandler(
        ICommunityRepository communityRepository,
        IValidator<CreateCommunityCommand> validator)
    {
        _communityRepository = communityRepository;
        _validator = validator;
    }

    public async Task<CreateCommunityResult> Handle(CreateCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if(_communityRepository.GetCommunityByName(command.Name) is not null)
            throw new HttpCustomException(
            HttpStatusCode.Conflict, $"Community {command.Name} already exists");

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

        return new CreateCommunityResult(
            community,
            "Community created successfully"
        );
    }
}