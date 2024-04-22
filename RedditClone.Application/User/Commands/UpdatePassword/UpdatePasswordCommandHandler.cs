namespace RedditClone.Application.User.Commands.UpdatePassword;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.UpdatePassword;

public partial class UpdatePasswordCommandHandler(
    IUserRepository userRepository)
        : IRequestHandler<UpdatePasswordCommand, ErrorOr<UpdatePasswordResult>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<UpdatePasswordResult>> Handle(
        UpdatePasswordCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@UpdatePasswordCommand}",
                "Trying to update password of: {@UserId}",
                command,
                command.UserId);

        _userRepository.UpdatePasswordById(
            command.UserId,
            command.OldPassword,
            command.NewPassword,
            command.MatchPassword);

        UpdatePasswordResult result = new("Password updated successfully");

        Log.Information(
            "{@UpdatePasswordResult}, {UserId}",
            result,
            command.UserId);

        return result;
    }
}