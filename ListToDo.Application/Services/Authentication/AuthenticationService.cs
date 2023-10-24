using ListTodo.Domain.Entities;
using ListToDo.Application.Common.Interfaces.Authentication;
using ListToDo.Application.Persistence;
using ListToDo.Application.Services.Authentication.Responses;

namespace ListToDo.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public LoginResponse Login(string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not User user){
            throw new Exception("User with this email not exists");
        }

        if(user.Password != password){
            throw new Exception("Invalid password");
        }

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName, email);

        return new LoginResponse(
            user.Id,
            email,
            token
        );
    }

    public RegisterResponse Register(string firstName, string lastName, string email, string password)
    {
        if(_userRepository.GetUserByEmail(email) is not null){
            throw new Exception("User email is already exist");
        }

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.Add(user);

        return new RegisterResponse(
            user.Id,
            firstName,
            lastName,
            email
        );
    }
}