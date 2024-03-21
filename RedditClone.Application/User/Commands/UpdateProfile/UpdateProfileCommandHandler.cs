namespace RedditClone.Application.User.Commands.UpdateProfile;

using MediatR;
using RedditClone.Application.Persistence;
using FluentValidation;
using RedditClone.Application.User.Results.UpdateProfile;
using Microsoft.Extensions.Configuration;
using RedditClone.Application.Helpers;
using Serilog;

public partial class UpdateProfileCommandHandler
    : IRequestHandler<UpdateProfileCommand, UpdateProfileResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdateProfileCommand> _validator;
    private readonly IConfiguration _configuration;

    public UpdateProfileCommandHandler(
        IUserRepository userRepository,
        IConfiguration configuration,
        IValidator<UpdateProfileCommand> validator)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _validator = validator;
    }

    public async Task<UpdateProfileResult> Handle(UpdateProfileCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@UpdateProfileCommand}",
            "Trying update profile of {@UserId}",
            command,
            command.UserId);

        _validator.ValidateAndThrow(command);

        _userRepository.UpdateProfileById(
            command.UserId,
            command.Firstname,
            command.Lastname,
            command.Email);

        UpdateProfileResult result = new("User profile updated");

        Log.Information(
            "{@UpdateProfileResult}, {@UserId}",
            result,
            command.UserId);

        return result;
    }
}