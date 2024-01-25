using ErrorOr;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Authentication.Results.Register;
using RedditClone.Application.Common.Interfaces.Authentication;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.Entities;
using RedditClone.Domain.UserAggregate.Entities;

namespace RedditClone.Application.Authentication.Commands.Register;

public class RegisterCommandHandler :
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
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicatedEmail;
        }

        if (_userRepository.GetUserByUsername(command.Username) is not null)
        {
            return Errors.User.DuplicatedUsername;
        }

        var user = UserAggregate.Create(
            command.FirstName,
            command.LastName,
            command.Username,
            command.Password,
            command.Email,
            command.CreatedAt,
            command.UpdatedAt
        );

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id.Value, user.FirstName, user.LastName);

        return new RegisterResult(
            user,
            token
        );
    }
}