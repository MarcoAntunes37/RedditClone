using ErrorOr;
using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Domain.Entities;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Authentication.Results.Register;
using RedditClone.Application.Common.Interfaces.Authentication;

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
        
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);
        
        return new RegisterResult(
            user.FirstName,
            user.LastName,
            user.Email,
            token
        );
    }
}