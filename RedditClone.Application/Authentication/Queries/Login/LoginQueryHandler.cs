using ErrorOr;
using MediatR;
using RedditClone.Application.Common.Interfaces.Authentication;
using RedditClone.Application.Persistence;
using RedditClone.Domain.Entities;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Authentication.Results.Login;
namespace RedditClone.Application.Authentication.Queries.Login;

public class LoginQueryHandler :
IRequestHandler<LoginQuery,
ErrorOr<LoginResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<LoginResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (user.Password != query.Password)
        {
            return new[] { Errors.Authentication.InvalidCredentials };
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName, user.Email);

        return new LoginResult(
            user.Email,
            token
        );
    }
}