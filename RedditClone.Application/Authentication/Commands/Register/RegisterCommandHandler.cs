using ErrorOr;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.Entities;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Authentication.Results.Register;

namespace RedditClone.Application.Authentication.Commands.Register;

public class RegisterCommandHandler :
IRequestHandler<RegisterCommand,
ErrorOr<RegisterResult>>
{
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<RegisterResult>> Handle(RegisterCommand command,
    CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicatedEmail;
        }

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        _userRepository.Add(user);

        return new RegisterResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email
        );
    }
}