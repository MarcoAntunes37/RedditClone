namespace RedditClone.Application.User.Queries.Login;

using FluentValidation;
using MediatR;
using RedditClone.Application.Common.Interfaces.Authentication;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.Login;
using RedditClone.Domain.UserAggregate;
using BCrypt.Net;
using RedditClone.Application.Common.Errors;
using System.Net;
using RedditClone.Application.Common.Helpers;
using Microsoft.Extensions.Configuration;
using Serilog;

public class LoginQueryHandler :
IRequestHandler<LoginQuery, LoginResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<LoginQuery> _validator;
    private IConfiguration _configuration;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IConfiguration configuration,
        IValidator<LoginQuery> validator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _configuration = configuration;
        _validator = validator;
    }

    public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        new SerilogLoggerConfiguration(_configuration).CreateLogger();

        Log.Information(
            "{@Message}, {@LoginQuery}",
                "Trying to retrieve user data",
                query);

        _validator.ValidateAndThrow(query);

        if (_userRepository.GetUserByEmail(query.Email) is not User user)
            throw new HttpCustomException(HttpStatusCode.Unauthorized, "Invalid credentials");

        if (!BCrypt.Verify(query.Password, user.Password))
            throw new HttpCustomException(HttpStatusCode.Unauthorized, "Invalid credentials");

        var token = _jwtTokenGenerator.GenerateToken(user.Id.Value, user.Firstname, user.Lastname);

        Log.Information(
            "User: {@UserName} successfully logged-in",
            user.Username);

        return new LoginResult(
            token
        );
    }
}