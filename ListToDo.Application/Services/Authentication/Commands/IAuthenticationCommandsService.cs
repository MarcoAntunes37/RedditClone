using ErrorOr;
using ListToDo.Application.Services.Authentication.Responses;

namespace ListToDo.Application.Services.Authentication.Commands;

public interface IAuthenticationCommandsService
{
    ErrorOr<RegisterResponse> Register(string firstName, string lastName, string email, string password);
}