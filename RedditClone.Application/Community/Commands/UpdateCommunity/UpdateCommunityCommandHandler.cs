namespace RedditClone.Application.Community.Commands.UpdateCommunity;

using FluentValidation;
using MediatR;
using RedditClone.Application.Community.Results.UpdateCommunityResult;
using RedditClone.Application.Persistence;

public class UpdateCommunityCommandHandler :
    IRequestHandler<UpdateCommunityCommand, UpdateCommunityResult>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IValidator<UpdateCommunityCommand> _validator;

    public UpdateCommunityCommandHandler(
        ICommunityRepository communityRepository,
        IValidator<UpdateCommunityCommand> validator)
    {
        _communityRepository = communityRepository;
        _validator = validator;
    }

    public async Task<UpdateCommunityResult> Handle(UpdateCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _communityRepository.UpdateCommunityById(command.CommunityId, command.UserId, command.Name, command.Description, command.Topic);

        var community = _communityRepository.GetCommunityById(command.CommunityId);
        return new UpdateCommunityResult(
            "Community successfully updated.",
            community
        );
    }
}