using ListToDo.Application.Common.Interfaces.Authentication;
using ListToDo.Application.Services.Authentication.Responses;

namespace ListToDo.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public LoginResponse Login(string email, string password)
    {
        Guid userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, "marco", "Antunes", email);

        return new LoginResponse(
            userId,
            email,
            token
        );
    }

    public RegisterResponse Register(string firstName, string lastName, string email, string password)
    {
        return new RegisterResponse(
            Guid.NewGuid(),
            firstName,
            lastName,
            email
        );
    }
}