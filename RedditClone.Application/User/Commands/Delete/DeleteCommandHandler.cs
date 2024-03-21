namespace RedditClone.Application.User.Commands.Delete;

using MediatR;
using RedditClone.Application.Persistence;
using FluentValidation;
using RedditClone.Application.User.Results;
using RedditClone.Application.Helpers;
using Microsoft.Extensions.Configuration;
using Serilog;

public partial class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, DeleteResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<DeleteUserCommand> _validator;
    private readonly IConfiguration _configuration;

    public DeleteUserCommandHandler(
        IUserRepository userRepository,
        IConfiguration configuration,
        IValidator<DeleteUserCommand> validator)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _validator = validator;
    }

    public async Task<DeleteResult> Handle(DeleteUserCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(
            _configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@DeleteUserCommand}",
                "Trying to delete user {@UserId}",
                command,
                command.UserId);

        _validator.ValidateAndThrow(command);

        _userRepository.DeleteUserById(command.UserId);

        var result = new DeleteResult("User deleted successfully");

        Log.Information(
            "{@DeleteResult}, {@UserId}",
            result,
            command.UserId);

        return result;
    }
}