namespace RedditClone.Application.User.Commands.Delete;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results;

public partial class DeleteUserCommandHandler(
    IUserRepository userRepository)
        : IRequestHandler<DeleteUserCommand, ErrorOr<DeleteResult>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<DeleteResult>> Handle(
        DeleteUserCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@DeleteUserCommand}",
                "Trying to delete user {@UserId}",
                command,
                command.UserId);

        _userRepository.DeleteUserById(command.UserId, command.CurrentUserId);

        DeleteResult result = new("User deleted successfully");

        Log.Information(
            "{@ErrorOr<DeleteResult>}, {@UserId}",
            result,
            command.UserId);

        return result;
    }
}