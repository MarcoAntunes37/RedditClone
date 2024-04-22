namespace RedditClone.Application.User.Commands.Register;

using MediatR;
using Serilog;
using ErrorOr;
using BCrypt.Net;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.Register;
using RedditClone.Application.Common.Interfaces.Authentication;

public partial class RegisterCommandHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator)
        : IRequestHandler<RegisterCommand, ErrorOr<RegisterResult>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ErrorOr<RegisterResult>> Handle(RegisterCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@RegisterCommand}",
                "Trying to register new user",
                command);

        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            Error error = Errors.User.EmailAlreadyExists;
            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }


        if (_userRepository.GetUserByUsername(command.Username) is not null)
        {
            Error error = Errors.User.UsernameAlreadyExists;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if (command.Password != command.RepeatPassword)
        {
            Error error = Errors.User.PasswordsDoNotMatch;

            Log.Error(
                "{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        var user = User.Create(
            command.Firstname,
            command.Lastname,
            command.Username,
            BCrypt.HashPassword(
                command.Password, BCrypt.GenerateSalt(), false, HashType.SHA256),
            command.Email
        );

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id.Value, user.Firstname, user.Lastname);

        RegisterResult result = new(user, token);

        Log.Information(
            "{@Message}, {@RegisterResult}",
            "User created successfully",
            result);

        return result;
    }
}