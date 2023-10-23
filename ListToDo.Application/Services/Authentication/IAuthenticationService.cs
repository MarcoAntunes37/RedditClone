using ListToDo.Application.Services.Authentication.Responses;

namespace ListToDo.Application.Services.Authentication;

public interface IAuthenticationService{
    LoginResponse Login(string email, string password);
    RegisterResponse Register(string firstName, string lastName, string email, string password);
}