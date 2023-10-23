using ListToDo.Application.Services.Authentication.Responses;

namespace ListToDo.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public LoginResponse Login(string email, string password)
    {
        return new LoginResponse(
            Guid.NewGuid(),
            email,
            "token"
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