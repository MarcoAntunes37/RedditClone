namespace RedditClone.Application.User.Commands.Register;

using MediatR;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.Register;
using RedditClone.Application.Common.Interfaces.Authentication;
using RedditClone.Domain.UserAggregate;
using BCrypt.Net;
using FluentValidation;
using RedditClone.Application.Errors;
using System.Net;

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

        _validator.ValidateAndThrow(command);

        if (_userRepository.GetUserByEmail(command.Email) is not null)
            throw new HttpCustomException(
            HttpStatusCode.Conflict, "Email is already in use");

        if (_userRepository.GetUserByUsername(command.Username) is not null)
            throw new HttpCustomException(
            HttpStatusCode.Conflict, "Username is already in use");

        if(command.Password != command.MatchPassword)
            throw new HttpCustomException(
            HttpStatusCode.BadRequest, "Password don't match with confirm password field");

        var user = User.Create(
            command.Firstname,
            command.Lastname,
            command.Username,
            BCrypt.HashPassword(
                command.Password, BCrypt.GenerateSalt(), false, HashType.SHA256),
            command.Email,
            command.CreatedAt,
            command.UpdatedAt
        );

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user.Id.Value, user.Firstname, user.Lastname);

        return new RegisterResult(
            user,
            token
        );
    }
}