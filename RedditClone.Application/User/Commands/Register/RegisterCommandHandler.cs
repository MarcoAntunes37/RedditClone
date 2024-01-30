using ErrorOr;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.Register;
using RedditClone.Application.Common.Interfaces.Authentication;
using RedditClone.Domain.Common.Errors.User;
using RedditClone.Domain.UserAggregate;
using System.Text.RegularExpressions;

namespace RedditClone.Application.User.Commands.Register;

public partial class RegisterCommandHandler :
IRequestHandler<RegisterCommand,
ErrorOr<RegisterResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<RegisterResult>> Handle(RegisterCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (string.IsNullOrEmpty(command.Email))
        {
            return Errors.RegisterUser.EmptyOrNullEmail;
        }

        Regex startWithValidCharacter = StartWithValidCharacter();
        Regex hasAt = HasAt();
        Regex isValidDomainLength = IsValidDomainLength();
        Regex isValidDomain = IsValidDomain();
        // if (!isValidEmail)
        // {
        //     return Errors.RegisterUser.NotValidEmail;
        // }

        if (string.IsNullOrEmpty(command.Username))
        {
            return Errors.RegisterUser.EmptyOrNullUsername;
        }

        if (string.IsNullOrEmpty(command.FirstName))
        {
            return Errors.RegisterUser.EmptyOrNullFirstName;
        }

        if (string.IsNullOrEmpty(command.LastName))
        {
            return Errors.RegisterUser.EmptyOrNullLastName;
        }

        if (string.IsNullOrEmpty(command.Password))
        {
            return Errors.RegisterUser.EmptyOrNullPassword;
        }

        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.RegisterUser.DuplicatedEmail;
        }

        if (_userRepository.GetUserByUsername(command.Username) is not null)
        {
            return Errors.RegisterUser.DuplicatedUsername;
        }

        var user = UserAggregate.Create(
            command.FirstName,
            command.LastName,
            command.Username,
            command.Password,
            command.Email,
            command.CreatedAt,
            command.UpdatedAt,
            command.UserCommunities
        );

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id.Value, user.FirstName, user.LastName);

        return new RegisterResult(
            user,
            token
        );
    }

    [GeneratedRegex("^[a-zA-Z0-9._%+-]+")]
    private static partial Regex StartWithValidCharacter();
    [GeneratedRegex("@")]
    private static partial Regex HasAt();
    [GeneratedRegex("[a-zA-Z]{2,}$")]
    private static partial Regex IsValidDomainLength();
    [GeneratedRegex("[a-zA-Z0-9.-]+\\.")]
    private static partial Regex IsValidDomain();
}