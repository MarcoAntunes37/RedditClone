namespace RedditClone.Application.Community.Commands.TransferCommunity;

using FluentValidation;
using MediatR;
using RedditClone.Application.Community.Results.TransferCommunityResult;
using RedditClone.Application.Persistence;

public class TransferCommunityCommandHandler :
    IRequestHandler<TransferCommunityCommand, TransferCommunityResult>
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IValidator<TransferCommunityCommand> _validator;

    public TransferCommunityCommandHandler(
        ICommunityRepository communityRepository,
        IValidator<TransferCommunityCommand> validator)
    {
        _communityRepository = communityRepository;
        _validator = validator;
    }

    public async Task<TransferCommunityResult> Handle(TransferCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _communityRepository.TransferCommunityOwnership(command.UserId, command.NewUserId, command.CommunityId);

        return new TransferCommunityResult(
            "Community successfully transferred."
        );
    }
}