using ErrorOr;
using ListToDo.Application.Services.Authentication.Responses;

namespace ListToDo.Application.Services.Authentication;

public interface IAuthenticationService
{
    ErrorOr<LoginResponse> Login(string email, string password);
    ErrorOr<RegisterResponse> Register(string firstName, string lastName, string email, string password);
}