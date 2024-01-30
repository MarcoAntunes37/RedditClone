using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.Register;
using RedditClone.Application.Common.Interfaces.Authentication;
using RedditClone.Domain.UserAggregate;
using FluentValidation;

namespace RedditClone.Application.User.Commands.Register;

public partial class RegisterCommandHandler :
IRequestHandler<RegisterCommand, RegisterResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IValidator<RegisterCommand> _validator;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IValidator<RegisterCommand> validator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _validator = validator;
    }

    public async Task<RegisterResult> Handle(RegisterCommand command,
    CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if(_userRepository.GetUserByEmail(command.Email) is not null)
            throw new Exception("Email is already in use");

        if(_userRepository.GetUserByUsername(command.Username) is not null)
            throw new Exception("Username is already in use");

        _validator.ValidateAndThrow(command);

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
}