namespace ListToDo.Application.Services.Authentication.Responses;

public record LoginResponse(
    Guid Id,
    string Email,
    string Token
);