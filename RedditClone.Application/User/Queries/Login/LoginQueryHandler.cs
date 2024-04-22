namespace RedditClone.Application.User.Queries.Login;

using Serilog;
using MediatR;
using ErrorOr;
using BCrypt.Net;
using RedditClone.Domain.UserAggregate;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.Login;
using RedditClone.Application.Common.Interfaces.Authentication;

public class LoginQueryHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository) :
IRequestHandler<LoginQuery, ErrorOr<LoginResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ErrorOr<LoginResult>> Handle(
        LoginQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@LoginQuery}",
                "Trying to retrieve user data",
                query);

        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            Error error = Errors.User.UserNotFound;

            Log.Error("{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if (!BCrypt.Verify(query.Password, user.Password))
        {
            Error error = Errors.User.InvalidCredentials;

            Log.Error("{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id.Value, user.Firstname, user.Lastname);

        Log.Information(
            "User: {@Username} successfully logged-in",
            user.Username);

        return new LoginResult(
            token
        );
    }
}