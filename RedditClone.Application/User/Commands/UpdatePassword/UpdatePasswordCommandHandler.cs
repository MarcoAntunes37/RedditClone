namespace RedditClone.Application.User.Commands.UpdatePassword;

using MediatR;
using RedditClone.Application.Persistence;
using FluentValidation;
using RedditClone.Application.User.Results.UpdatePassword;
using RedditClone.Application.Helpers;
using Microsoft.Extensions.Configuration;
using Serilog;

public partial class UpdatePasswordCommandHandler
    : IRequestHandler<UpdatePasswordCommand, UpdatePasswordResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdatePasswordCommand> _validator;
    private readonly IConfiguration _configuration;

    public UpdatePasswordCommandHandler(
        IUserRepository userRepository,
        IConfiguration configuration,
        IValidator<UpdatePasswordCommand> validator)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _validator = validator;
    }

    public async Task<UpdatePasswordResult> Handle(UpdatePasswordCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(
           _configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@UpdatePasswordCommand}",
                "Trying to update password of: {@UserId}",
                command,
                command.UserId);

        _validator.ValidateAndThrow(command);

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