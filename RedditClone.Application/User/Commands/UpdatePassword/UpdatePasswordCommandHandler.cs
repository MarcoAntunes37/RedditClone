namespace RedditClone.Application.User.Commands.UpdatePassword;

using MediatR;
using RedditClone.Application.Persistence;
using FluentValidation;
using RedditClone.Application.User.Results.UpdatePassword;

public partial class UpdatePasswordCommandHandler
    : IRequestHandler<UpdatePasswordCommand, UpdatePasswordResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdatePasswordCommand> _validator;

    public UpdatePasswordCommandHandler(
        IUserRepository userRepository,
        IValidator<UpdatePasswordCommand> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<UpdatePasswordResult> Handle(UpdatePasswordCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _userRepository.UpdatePasswordById(
            command.UserId,
            command.OldPassword,
            command.NewPassword,
            command.MatchPassword);

        return new UpdatePasswordResult(
            "Password updated successfully"
        );
    }
}