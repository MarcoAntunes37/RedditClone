namespace RedditClone.Application.Community.Commands.Delete;

using FluentValidation;
using MediatR;
using RedditClone.Application.Community.Results.DeleteCommunityResult;
using RedditClone.Application.Persistence;

public class DeleteCommunityCommandHandler :
    IRequestHandler<DeleteCommunityCommand, DeleteCommunityResult>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IValidator<DeleteCommunityCommand> _validator;

    public DeleteCommunityCommandHandler(
        ICommunityRepository communityRepository,
        IValidator<DeleteCommunityCommand> validator)
    {
        _communityRepository = communityRepository;
        _validator = validator;
    }

    public async Task<DeleteCommunityResult> Handle(DeleteCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _communityRepository.DeleteCommunityById(command.CommunityId, command.UserId);

        return new DeleteCommunityResult(
            "Community successfully Deleted."
        );
    }
}