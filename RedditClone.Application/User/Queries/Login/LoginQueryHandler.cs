namespace RedditClone.Application.User.Queries.Login;

using FluentValidation;
using MediatR;
using RedditClone.Application.Common.Interfaces.Authentication;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.Login;
using RedditClone.Domain.UserAggregate;
using BCrypt.Net;
using RedditClone.Application.Errors;
using System.Net;

public class LoginQueryHandler :
IRequestHandler<LoginQuery, LoginResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<LoginQuery> _validator;
    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IValidator<LoginQuery> validator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(query);

        if (_userRepository.GetUserByEmail(query.Email) is not User user)
            throw new HttpCustomException(
            HttpStatusCode.Unauthorized, "Invalid credentials");

        if (!BCrypt.Verify(query.Password, user.Password))
            throw new HttpCustomException(
            HttpStatusCode.Unauthorized, "Invalid credentials");

        var token = _jwtTokenGenerator.GenerateToken(user.Id.Value, user.Firstname, user.Lastname);

        return new LoginResult(
            token
        );
    }
}