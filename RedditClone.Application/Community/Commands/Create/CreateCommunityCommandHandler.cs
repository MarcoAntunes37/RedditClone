namespace RedditClone.Application.Community.Commands.Create;

using FluentValidation;
using MediatR;
using RedditClone.Application.Community.Results.CreateCommunityResult;
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

        _validator.ValidateAndThrow(command);

        //create community
        var community = Community.Create(
            command.UserId,
            command.Name,
            command.Description,
            command.Topic,
            command.CreatedAt,
            command.UpdatedAt
        );

        //persist community
        _communityRepository.Add(community);

        //return community
        return new CreateCommunityResult(
            community
        );
    }
}