using ErrorOr;
using ListToDo.Application.Services.Authentication.Responses;

namespace ListToDo.Application.Services.Authentication.Queries;

public interface IAuthenticationQueriesService
{
    ErrorOr<LoginResponse> Login(string email, string password);
}