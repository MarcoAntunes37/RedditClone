namespace RedditClone.Application.User.Commands.Delete;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results;
using RedditClone.Domain.Common.Errors;

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

        if(command.CurrentUserId != command.UserId)
        {
            Error error = Errors.User.OnlyDeleteSelf;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        var success = _userRepository.DeleteUserById(command.UserId, command.CurrentUserId);

        if (success != true)
        {
            return success.FirstError;
        }

        DeleteResult result = new("User deleted successfully");

        Log.Information(
            "{@DeleteResult}, {@UserId}",
            result,
            command.UserId);

        return result;
    }
}