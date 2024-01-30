using FluentValidation;
using MediatR;
using RedditClone.Application.Common.Interfaces.Authentication;
using RedditClone.Application.Persistence;
using RedditClone.Application.User.Results.Login;
using RedditClone.Domain.UserAggregate;

namespace RedditClone.Application.User.Queries.Login;

public class LoginQueryHandler :
IRequestHandler<LoginQuery, LoginResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<LoginQuery> _validator;
    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator,
    IUserRepository userRepository,
    IValidator<LoginQuery> validator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(query);

        if (_userRepository.GetUserByEmail(query.Email) is not UserAggregate user)
            throw new Exception("Invalid credentials");

        if (user.Password != query.Password)
            throw new Exception("Invalid credentials");


        var token = _jwtTokenGenerator.GenerateToken(user.Id.Value, user.FirstName, user.LastName);

        return new LoginResult(
            token
        );
    }
}