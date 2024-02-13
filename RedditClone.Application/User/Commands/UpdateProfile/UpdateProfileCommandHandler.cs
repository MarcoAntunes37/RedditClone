namespace RedditClone.Application.User.Commands.UpdateProfile;

using MediatR;
using RedditClone.Application.Persistence;
using FluentValidation;
using RedditClone.Application.User.Results.UpdateProfile;

public partial class UpdateProfileCommandHandler
    : IRequestHandler<UpdateProfileCommand, UpdateProfileResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdateProfileCommand> _validator;

    public UpdateProfileCommandHandler(
        IUserRepository userRepository,
        IValidator<UpdateProfileCommand> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<UpdateProfileResult> Handle(UpdateProfileCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _userRepository.UpdateProfileById(
            command.UserId,
            command.Firstname,
            command.Lastname,
            command.Email);

        return new UpdateProfileResult(
            "Profile updated"
        );
    }
}